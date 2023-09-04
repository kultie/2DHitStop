using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.TimeSystem;
using UnityEngine;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public class Streak : IStreak
    {
        private float _remainTime;
        public int Count { get; set; }
        public float Timeout { get; set; }
        public int Remains { get; private set; }
        public float RemainPercent => _remainTime / Timeout;
        private CoroutineReference _coroutine;

        public Streak(int count, float timeout)
        {
            Count = count;
            Timeout = timeout;
        }

        public bool Consume()
        {
            if (Remains <= 0)
            {
                return false;
            }

            Remains--;
            return true;
        }

        public void Start()
        {
            if (Count == 0) return;
            _coroutine.Stop();
            _coroutine = CoroutineProxy.Schedule(Update());
        }

        IEnumerator Update()
        {
            Remains = Count;
            _remainTime = Timeout;
            while (_remainTime > 0)
            {
                _remainTime -= TimeKeeper.Global.DeltaTime;
                yield return null;
            }

            Remains = 0;
        }

        public void Expire()
        {
            _coroutine.Stop();
            Remains = 0;
            _remainTime = 0;
        }
    }
}