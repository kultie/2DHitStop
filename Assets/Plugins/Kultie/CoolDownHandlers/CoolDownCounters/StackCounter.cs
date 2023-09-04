using System;
using UnityEngine;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public class StackCounter : ICoolDownCounter
    {
        private Action _onReady;

        public event Action OnReady
        {
            add => _onReady += value;
            remove => _onReady -= value;
        }

        private int _currentStack;
        private int _maxStack;
        public int MaxCounter => _maxStack;

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

        public float RemainPercent => 0;

        public StackCounter(int maxStack, int startingStack)
        {
            _maxStack = maxStack;
            RemainCounter = startingStack;
        }

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
            RemainCounter += 1;
            RemainCounter = Mathf.Clamp(RemainCounter, 0, MaxCounter);
        }

        public bool IsValid()
        {
            return RemainCounter > 0;
        }

        public void SetStack(int value)
        {
            RemainCounter = value;
            RemainCounter = Mathf.Clamp(RemainCounter, 0, MaxCounter);
        }

        public void Reset()
        {
        }
        
        public bool CanCalculateCoolDown()
        {
            return RemainCounter < MaxCounter;
        }
    }
}