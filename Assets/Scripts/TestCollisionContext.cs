using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.CollisionSystem;
using Kultie.GameMechanics.HitStopSystem;
using UnityEngine;

namespace Kultie.GameMechanics.Test
{
    public class TestCollisionContext : ICollisionContext
    {
        private CharacterController _attacker;
        private CharacterController _victim;
        public HitStopData HitStopData;

        public TestCollisionContext(CharacterController attacker, CharacterController victim, HitStopData hitStopData)
        {
            _attacker = attacker;
            _victim = victim;
            HitStopData = hitStopData;
        }

        public bool IsValid()
        {
            return true;
        }

        public void OnCollide()
        {
            HitStopData.Trigger(_attacker, _victim);
        }
    }
}