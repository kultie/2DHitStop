using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.CollisionSystem
{
    public abstract class HurtBox : MonoBehaviour
    {
        public abstract void OnHitBy(ICollisionContext context);
    }

    public abstract class HurtBox<TContext> : HurtBox where TContext : ICollisionContext
    {
        public override void OnHitBy(ICollisionContext context)
        {
            OnHitBy((TContext)context);
        }

        protected abstract void OnHitBy(TContext context);
    }
}