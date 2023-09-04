using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public interface IStreak
    {
        /// <summary>
        /// Max count for streak
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// Timeout to reset streak remains to 0
        /// </summary>
        float Timeout { get; set; }

        /// <summary>
        /// The remains streak that can consume
        /// </summary>
        int Remains { get; }

        /// <summary>
        /// Percent of remain time (Useful for UI)
        /// </summary>
        float RemainPercent { get; }

        /// <summary>
        /// Function to validate the consume command of ICoolDown
        /// </summary>
        /// <returns>
        /// Determined the call to consume is success or not
        /// If remain > 1  return true
        /// </returns>
        bool Consume();

        /// <summary>
        /// Function to start counting down the time remaining for the streak to expire
        /// </summary>
        void Start();

        /// <summary>
        /// Function to run when the streak has timed out
        /// </summary>
        void Expire();
    }
}