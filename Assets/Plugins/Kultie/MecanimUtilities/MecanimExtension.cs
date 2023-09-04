using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.MecanimUtilities
{
    public static class MecanimExtension
    {
        public static WaitForAnimationToFinish PlayAndWait(this Animator animator, string animationName, int layer = 0)
        {
            animator.Play(animationName, layer);
            return new WaitForAnimationToFinish(animator, animationName, layer);
        }
    }
}