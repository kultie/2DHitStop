using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.TimeSystem
{
    public class TimedEntity : MonoBehaviour, ITimeInstance
    {
        private ITimeInstance referenceInstance;
        [SerializeField] [Range(0, 2f)] private float _timeScale = 1;

        public void SetReference(ITimeInstance instance)
        {
            referenceInstance = instance;
        }

        public void SetTimeScale(float value)
        {
            _timeScale = value;
        }

        public virtual float TimeScale => _timeScale * (referenceInstance?.TimeScale ?? 1);
        public float DeltaTime => Time.deltaTime * TimeScale;

        float TimeScaleFunc() => TimeScale;

        /// <summary>
        /// Wait using time scale from timed resolver
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public WaitInstruction Wait(float time)
        {
            return new WaitInstruction(time, TimeScaleFunc);
        }
    }
}