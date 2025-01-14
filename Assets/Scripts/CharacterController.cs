using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.AnimationSystem;
using Kultie.GameMechanics.HitStopSystem;
using Kultie.GameMechanics.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kultie.GameMechanics.Test
{
    public class CharacterController : TimedEntity, IHitStopSubject
    {
        [SerializeField]
        [Range(0, 5)] private float attackSpeed = 1;
        public IAnimator Animator { private set; get; }
        private float _hitStopTime = 1;
        public override float TimeScale => base.TimeScale * _hitStopTime;

        private void Awake()
        {
            Animator = GetComponentInChildren<IAnimator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Attack1();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Attack2();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Attack3();
            }
        }

        [Button]
        void Attack1()
        {
            StopHitStop();
            Animator.PlayAnimation("Attack1", ReturnToIdle, attackSpeed);
        }

        [Button]
        void Attack2()
        {
            StopHitStop();
            Animator.PlayAnimation("Attack2", ReturnToIdle, attackSpeed);
        }

        [Button]
        void Attack3()
        {
            StopHitStop();
            Animator.PlayAnimation("Attack3", ReturnToIdle, attackSpeed);
        }

        void ReturnToIdle()
        {
            Animator.PlayLoopAnimation("Idle");
        }

        public void SetHitStop(float value)
        {
            _hitStopTime = value;
        }

        private IEnumerator hitStopSeq;

        public void TriggerHitStop(int frameCount)
        {
            if (hitStopSeq != null)
            {
                StopCoroutine(hitStopSeq);
            }

            hitStopSeq = HitStopSequence(frameCount);
            StartCoroutine(hitStopSeq);
        }

        IEnumerator HitStopSequence(int frameCount)
        {
            int _frame = frameCount;
            SetHitStop(0);

            while (_frame > 0)
            {
                //Wait for 1 frame of update
                yield return TimeKeeper.Global.Wait(Time.deltaTime);
                _frame--;
            }

            SetHitStop(1);
        }

        void StopHitStop()
        {
            if (hitStopSeq != null)
            {
                StopCoroutine(hitStopSeq);
            }

            SetHitStop(1);
        }
    }
}