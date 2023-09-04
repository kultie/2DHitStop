using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public interface ICoolDown
    {
        /// <summary>
        /// Event for when this is ready to use
        /// </summary>
        event Action OnReady;

        /// <summary>
        /// Condition for if this can be use
        /// </summary>
        bool CanUse { get; }

        /// <summary>
        /// Useful for displaying the fill rate of cooldown image/icon
        /// </summary>
        float RemainPercent { get; }

        /// <summary>
        /// Useful for type of ability that can be use multiple time after initial cast
        /// Cannot be null
        /// </summary>
        ICoolDownCounter Counter { get; }

        /// <summary>
        /// Function to trigger the function when required
        /// </summary>
        /// <returns>Determined the call to consume is success or not
        ///Example for common case:
        /// If allow to cast return true
        /// If streak is successfully consume return true
        /// If stack is successfully consume return true
        /// </returns>
        bool Consume();

        /// <summary>
        /// Use for when and function that can reduce cooldown by fixed amount of time
        /// </summary>
        /// <param name="time">The time for cooldown reduce</param>
        /// <returns></returns>
        void ReduceCoolDown(float time);

        /// <summary>
        /// Use for when and function that can reduce cooldown by a rate base on some value that class has
        /// </summary>
        /// <param name="time">The rate for cooldown reduce. Use value from 0~1 (0.5 = 50%)</param>
        /// <returns></returns>
        void ReduceCoolDownRate(float rate);
    }
}