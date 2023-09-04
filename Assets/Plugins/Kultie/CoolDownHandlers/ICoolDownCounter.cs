using System;

namespace Kultie.GameMechanics.CoolDownSystem
{
    /// <summary>
    /// Base interface to show player the extra number for the cooling down action
    /// Useful for stackable and streak-able cooldown casting
    /// </summary>
    public interface ICoolDownCounter
    {
        event Action OnReady;

        /// <summary>
        /// Maximum counter of this counter
        /// </summary>
        int MaxCounter { get; }

        /// <summary>
        /// Current counter
        /// </summary>
        int RemainCounter { set; get; }

        /// <summary>
        /// Percent useful for UI to indicate special state for using this ability
        /// </summary>
        float RemainPercent { get; }

        /// <summary>
        /// Function to consume the counter
        /// </summary>
        /// <returns>Return true if successfully consume the counter</returns>
        bool Consume();

        /// <summary>
        /// Function to gain the counter base on request
        /// </summary>
        /// <param name="value"></param>
        void GainCounter(int value);

        /// <summary>
        /// Function use for class that implemented ICoolDown
        /// Get called when a cooldown cycle is finish
        /// </summary>
        void OnCompleteCoolDownCycle();

        /// <summary>
        /// If the counter is valid to consume
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        void Reset();


        /// <summary>
        /// If the class implemented ICoolDown can start a cooldown cycle
        /// </summary>
        /// <returns></returns>
        bool CanCalculateCoolDown();
    }
}