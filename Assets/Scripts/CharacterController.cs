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
        public IAnimator Animator { private set; get; }
        private float _hitStopTime = 1;
        public override float TimeScale => base.TimeScale * _hitStopTime;

        private void Awake()
        {
            Animator = GetComponentInChildren<IAnimator>();
        }

        [Button]
        void Attack1()
        {
            Animator.PlayAnimation("Attack1", ReturnToIdle);
        }

        [Button]
        void Attack2()
        {
            Animator.PlayAnimation("Attack2", ReturnToIdle);
        }

        [Button]
        void Attack3()
        {
            Animator.PlayAnimation("Attack3", ReturnToIdle);
        }

        void ReturnToIdle()
        {
            Animator.PlayLoopAnimation("Idle");
        }

       public void SetHitStop(float value)
        {
            _hitStopTime = value;
        }
    }
}