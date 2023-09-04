using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.CollisionSystem;
using UnityEngine;

namespace Kultie.GameMechanics.Test
{
    public class TestCollisionContext : ICollisionContext
    {
        private CharacterController _attacker;
        private CharacterController _victim;
        public int hitStopFrame;

        public TestCollisionContext(CharacterController attacker, CharacterController victim, int hitStopFrame)
        {
            _attacker = attacker;
            _victim = victim;
            this.hitStopFrame = hitStopFrame;
        }

        public bool IsValid()
        {
            return true;
        }

        public void OnCollide()
        {
            CoroutineProxy.Schedule(HitStopSequence());
        }

        IEnumerator HitStopSequence()
        {
            var frameCount = hitStopFrame;
            _attacker.SetHitStop(0);
            _victim.SetHitStop(0);
            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }

            _attacker.SetHitStop(1);
            _victim.SetHitStop(1);
        }
    }
}