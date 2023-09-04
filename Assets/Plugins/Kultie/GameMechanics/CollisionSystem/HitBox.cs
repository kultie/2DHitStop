using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kultie.GameMechanics.CollisionSystem
{
    public abstract class HitBox<TContext, THurtBox> : MonoBehaviour where TContext : ICollisionContext where THurtBox: HurtBox
    {
        [SerializeField] private Collider2D collider;
        [SerializeField] private LayerMask maskToCheck;

        [SerializeField] private bool multipleHit;

        [ShowIf("@this.multipleHit")] [SerializeField]
        private float checkInterval = 0.05f;

        private IEnumerator _seq;

        private void OnEnable()
        {
            QueryCollision();
            _seq = CheckSequence();
            if (multipleHit)
            {
                StartCoroutine(_seq);
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_seq);
        }

        protected virtual IEnumerator CheckSequence()
        {
            while (true)
            {
                yield return IntervalWait(checkInterval);
                QueryCollision();
            }
        }

        protected abstract WaitInstruction IntervalWait(float time);

        void QueryCollision()
        {
            List<Collider2D> results = new List<Collider2D>();
            collider.OverlapCollider(new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = maskToCheck
            }, results);
            foreach (var a in results)
            {
                var hurtBox = a.GetComponent<THurtBox>();
                if (hurtBox)
                {
                    var context = GetCollisionContext(hurtBox);
                    if (HitFilter(hurtBox) && context.IsValid())
                    {
                        context.OnCollide();
                        hurtBox.OnHitBy(context);
                    }
                }
            }
        }

        protected abstract bool HitFilter(THurtBox hitSubject);
        protected abstract TContext GetCollisionContext(THurtBox hitSubject);
    }
}