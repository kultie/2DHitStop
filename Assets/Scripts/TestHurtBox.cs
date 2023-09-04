using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.CollisionSystem;
using Kultie.GameMechanics.TimeSystem;
using UnityEngine;

namespace Kultie.GameMechanics.Test
{
    public class TestHurtBox : HurtBox<TestCollisionContext>
    {
        [SerializeField] private SpriteRenderer renderer;
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
        protected override void OnHitBy(TestCollisionContext context)
        {
            StartCoroutine(HitSequence());
        }

        IEnumerator HitSequence()
        {
            renderer.material.EnableKeyword("HITEFFECT_ON");
            renderer.material.EnableKeyword("SHAKEUV_ON");
            yield return TimeKeeper.Global.Wait(0.25f);
            renderer.material.DisableKeyword("HITEFFECT_ON");
            renderer.material.DisableKeyword("SHAKEUV_ON");
        }
    }
}