using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.CollisionSystem;
using Kultie.GameMechanics.HitStopSystem;
using Kultie.GameMechanics.TimeSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kultie.GameMechanics.Test
{
    public class TestHitBox : HitBox<TestCollisionContext, TestHurtBox>
    {
        private CharacterController _characterController;
        
        public CharacterController Owner
        {
            get
            {
                if (!_characterController)
                {
                    _characterController = GetComponentInParent<CharacterController>();
                }

                return _characterController;
            }
        }

        [FormerlySerializedAs("hitStopFrame")] [SerializeField] private HitStopData hitStopData;

        protected override void OnActive()
        {
            
        }

        protected override WaitInstruction IntervalWait(float time)
        {
            return Owner.Wait(time);
        }

        protected override bool HitFilter(TestHurtBox hitSubject)
        {
            return hitSubject.Owner != Owner;
        }

        protected override TestCollisionContext GetCollisionContext(TestHurtBox hitSubject)
        {
            return new TestCollisionContext(Owner, hitSubject.Owner, hitStopData);
        }
    }
}