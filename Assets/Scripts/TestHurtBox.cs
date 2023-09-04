using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.CollisionSystem;
using Kultie.GameMechanics.TimeSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kultie.GameMechanics.Test
{
    public class TestHurtBox : HurtBox<TestCollisionContext>
    {
        [SerializeField] private SpriteRenderer renderer;
        private CharacterController _characterController;
        private IEnumerator seq;

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

        protected override void OnHitBy(TestCollisionContext context)
        {
            if (seq != null)
            {
                StopCoroutine(seq);
            }

            seq = HitSequence(context);
            StartCoroutine(seq);
        }

        IEnumerator HitSequence(TestCollisionContext context)
        {
            int frame = context.HitStopData.HitStun;
            Owner.Animator.PlayAnimation($"Hurt{Random.Range(1, 3)}");
            renderer.material.EnableKeyword("HITEFFECT_ON");
            renderer.material.EnableKeyword("SHAKEUV_ON");
            while (frame > 0)
            {
                frame--;
                yield return TimeKeeper.Global.Wait(Time.deltaTime);
            }

            renderer.material.DisableKeyword("HITEFFECT_ON");
            renderer.material.DisableKeyword("SHAKEUV_ON");
            Owner.Animator.PlayLoopAnimation("Idle");
        }
    }
}