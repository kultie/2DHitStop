using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.TimeSystem
{
    public class WaitInstruction : CustomYieldInstruction
    {
        private float _time;
        private Func<float> _timeScaleFunc;

        public override bool keepWaiting
        {
            get
            {
                try
                {
                    _time -= TimeKeeper.Global.DeltaTime * _timeScaleFunc();
                }
                catch (Exception exception)
                {
                    _time -= TimeKeeper.Global.DeltaTime;
                }

                return _time > 0;
            }
        }

        public WaitInstruction(float time, Func<float> timeScaleFunc)
        {
            _time = time;
            _timeScaleFunc = timeScaleFunc;
        }
    }
}