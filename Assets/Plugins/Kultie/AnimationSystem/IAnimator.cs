using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.AnimationSystem
{
    public interface IAnimator
    {
        IEnumerator PlayAnimationCoroutine(string animationKey, Action onComplete = null, float speed = 1);
        void PlayAnimation(string animationKey, Action onComplete = null, float speed = 1);
        void PlayLoopAnimation(string animationKey);
    }
}