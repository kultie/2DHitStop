using System;
using UnityEngine;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public class DefaultCounter : ICoolDownCounter
    {
        private Action _onReady;

        public event Action OnReady
        {
            add => _onReady += value;
            remove => _onReady -= value;
        }

        private int _currentStack;
        public int MaxCounter => 1;

        public int RemainCounter
        {
            get => _currentStack;
            set
            {
                if (_currentStack == 0 && value > 0)
                {
                    _onReady?.Invoke();
                }

                _currentStack = value;
            }
        }

        public bool CanCalculateCoolDown()
        {
            return RemainCounter < MaxCounter;
        }

        public float RemainPercent => 0;

        public bool Consume()
        {
            if (RemainCounter <= 0)
            {
                return false;
            }

            RemainCounter--;
            return true;
        }

        public void GainCounter(int value)
        {
            RemainCounter += value;
            RemainCounter = Mathf.Clamp(RemainCounter, 0, MaxCounter);
        }

        public void OnCompleteCoolDownCycle()
        {
            RemainCounter = MaxCounter;
        }

        public bool IsValid()
        {
            return RemainCounter > 0;
        }

        public void Reset()
        {
        }
    }
}