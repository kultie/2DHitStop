using System;
using System.Collections;
using Kultie.GameMechanics.TimeSystem;
using UnityEngine;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public class StreakCounter : ICoolDownCounter
    {
        public event Action OnReady;

        private int _maxStreak;
        private int _currentStreakCount;
        private float _remainTime;
        private float _timeOut;
        public int MaxCounter => _maxStreak;
        private CoroutineReference _coroutine;

        public int RemainCounter
        {
            get => _currentStreakCount;
            set => _currentStreakCount = value;
        }

        public float RemainPercent => _remainTime / _timeOut;

        public void Reset()
        {
            RemainCounter = _maxStreak;
            _remainTime = 0;
            _coroutine.Stop();
        }

        public void GainCounter(int value)
        {
            RemainCounter += value;
            RemainCounter = Mathf.Clamp(RemainCounter, 0, MaxCounter);
        }

        public void OnCompleteCoolDownCycle()
        {
            RemainCounter = _maxStreak;
        }

        public bool IsValid()
        {
            return RemainCounter > 0;
        }

        public StreakCounter(int maxStreak, float streakTimeout)
        {
            _maxStreak = maxStreak;
            _timeOut = streakTimeout;
        }

        public bool Consume()
        {
            if (RemainCounter <= 0) return false;
            RemainCounter--;

            if (_remainTime <= 0)
            {
                _coroutine.Stop();
                _coroutine = CoroutineProxy.Schedule(TimingOut());
            }

            return true;
        }

        IEnumerator TimingOut()
        {
            _remainTime = _timeOut;
            while (_remainTime > 0)
            {
                _remainTime -= TimeKeeper.Global.DeltaTime;
                yield return null;
            }

            Expire();
        }

        public void Expire()
        {
            _coroutine.Stop();
            RemainCounter = 0;
            _remainTime = 0;
        }

        public bool CanCalculateCoolDown()
        {
            return RemainCounter <= 0;
        }
    }
}