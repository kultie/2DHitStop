using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.TimeSystem;
using UnityEngine;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public class TimedCoolDown : ICoolDown
    {
        public delegate float CoolDownSpeedFunc();

        private static CoolDownSpeedFunc _defaultCoolDownSpeedFunc = () => 1f;

        private CoolDownSpeedFunc coolDownSpeedFunc;

        private float _remainTime;
        private float _coolDownTime;
        private Action _onReady;
        private ICoolDownCounter _counter;

        public event Action OnReady
        {
            add => Counter.OnReady += value;
            remove => Counter.OnReady -= value;
        }

        public bool CanUse => Counter.IsValid();

        public float RemainPercent => Counter.IsValid() ? 0f : _remainTime / _coolDownTime;
        public ICoolDownCounter Counter => _counter;
        private CoroutineReference _coroutine;

        public TimedCoolDown(float coolDownTime, ICoolDownCounter coolDownCounter,
            CoolDownSpeedFunc coolDownFunc = null)
        {
            _coolDownTime = coolDownTime;
            coolDownSpeedFunc = coolDownFunc ?? _defaultCoolDownSpeedFunc;
            _counter = coolDownCounter;
            _coroutine.Stop();
            _coroutine = CoroutineProxy.Schedule(UpdateCoolDown());
        }

        IEnumerator UpdateCoolDown()
        {
            // while (true)
            // {
            //     while (_remainTime > 0.0)
            //     {
            //         while (_stack >= _maxStack || _streak.Remains > 0)
            //         {
            //             yield return null;
            //         }
            //
            //         _remainTime -= TimeKeeper.Global.DeltaTime * coolDownSpeedFunc();
            //         _remainTime = Mathf.Clamp(_remainTime, 0, _coolDownTime);
            //         yield return null;
            //     }
            //     _remainTime = _coolDownTime;
            //     _stack++;
            // }

            while (true)
            {
                do
                {
                    if (_counter.CanCalculateCoolDown())
                    {
                        _remainTime -= TimeKeeper.Global.DeltaTime * coolDownSpeedFunc();
                    }

                    yield return null;
                } while (_remainTime > 0);

                _remainTime = _coolDownTime;
                Counter.OnCompleteCoolDownCycle();
            }
        }

        public bool Consume()
        {
            if (Counter.Consume())
            {
                return true;
            }

            return true;
        }

        public void ReduceCoolDown(float time)
        {
            if (_remainTime <= 0) return;
            _remainTime -= time;
            _remainTime = Mathf.Max(_remainTime, 0);
        }

        public void ReduceCoolDownRate(float rate)
        {
            if (_remainTime <= 0) return;
            var time = rate * _coolDownTime;
            _remainTime -= time;
            _remainTime = Mathf.Max(_remainTime, 0);
        }
    }
}