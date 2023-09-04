using System.Collections;
using System;
using Kultie.GameMechanics.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kultie.AnimationSystem
{
    public class MecanimAnimator : MonoBehaviour, IAnimator
    {
        [Required] [SerializeField] private Animator anim;
        private TimedEntity _owner;

        private void Start()
        {
            _owner = GetComponentInParent<TimedEntity>();
        }

        public IEnumerator PlayAnimationCoroutine(string animationKey, Action onComplete = null, float speed = 1)
        {
            var time = StartAnimation(animationKey);
            if (time == 0)
            {
                onComplete?.Invoke();
                yield break;
            }

            yield return _owner.Wait(time);
            onComplete?.Invoke();
        }

        public void PlayAnimation(string animationKey, Action onComplete = null, float speed = 1)
        {
            StartCoroutine(PlayAnimationCoroutine(animationKey, onComplete, speed));
        }

        public void PlayLoopAnimation(string animationKey)
        {
            StartAnimation(animationKey);
        }

        float StartAnimation(string key)
        {
            int stateID = Animator.StringToHash(key);
            if (anim.HasState(0, stateID))
            {
                anim.CrossFadeInFixedTime(key, 0);
                return anim.GetCurrentAnimatorStateInfo(0).length;
            }


            Debug.LogWarning($"No animation state {key} found");
            return 0;
        }

        private void Update()
        {
            anim.speed = _owner.TimeScale;
        }
    }
}