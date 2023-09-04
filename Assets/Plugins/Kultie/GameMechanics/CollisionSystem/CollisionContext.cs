using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.CollisionSystem
{
    public interface ICollisionContext
    {
        bool IsValid();
        void OnCollide();
    }
}