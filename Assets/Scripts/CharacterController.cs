using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.AnimationSystem;
using Kultie.GameMechanics.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kultie.GameMechanics.Test
{
    public class CharacterController : TimedEntity
    {
        private IAnimator _animator;
        private float _hitStopTime = 1;
        public override float TimeScale => base.TimeScale * _hitStopTime;

        private void Awake()
        {
            _animator = GetComponentInChildren<IAnimator>();
        }

        [Button]
        void Attack1()
        {
            _animator.PlayAnimation("Attack1", ReturnToIdle);
        }

        [Button]
        void Attack2()
        {
            _animator.PlayAnimation("Attack2", ReturnToIdle);
        }

        [Button]
        void Attack3()
        {
            _animator.PlayAnimation("Attack3", ReturnToIdle);
        }

        void ReturnToIdle()
        {
            _animator.PlayLoopAnimation("Idle");
        }

       public void SetHitStop(float value)
        {
            _hitStopTime = value;
        }
    }
}