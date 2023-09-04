using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kultie.GameMechanics.TimeSystem
{
    [DefaultExecutionOrder(-99)]
    public class TimeKeeper : MonoBehaviour, ITimeInstance
    {
        private const string GLOBAL_KEY = "Global";
        private static readonly Dictionary<string, TimeKeeper> Keepers = new();

        [Required] [InfoBox("DO NOT put Global here", InfoMessageType.Warning)] [SerializeField]
        private string key;

        [SerializeField] private TimeKeeper referenceKeeper;
        [SerializeField] [Range(0, 2f)] private float _timeScale = 1;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void CreateGlobalKeeper()
        {
            var globalInstance = new GameObject("Global Time Keeper", typeof(TimeKeeper)).GetComponent<TimeKeeper>();
            globalInstance.key = GLOBAL_KEY;
            Keepers[GLOBAL_KEY] = globalInstance;
            DontDestroyOnLoad(globalInstance.gameObject);
        }

        public static void CreateTimeKeeper(string key)
        {
            var instance = new GameObject($"{key} Time Keeper", typeof(TimeKeeper)).GetComponent<TimeKeeper>();
            instance.key = key;
            Keepers[key] = instance;
        }

        public static TimeKeeper Global => Keepers[GLOBAL_KEY];

        public static TimeKeeper GetTimeKeeper(string key)
        {
            if (Keepers.TryGetValue(key, out var result))
            {
                return result;
            }

            return null;
        }

        private void Awake()
        {
            if (string.IsNullOrEmpty(key)) return;
            if (key != GLOBAL_KEY && !referenceKeeper)
            {
                Keepers[key] = this;
                referenceKeeper = Keepers[GLOBAL_KEY];
            }
        }

        private void OnDestroy()
        {
            Keepers.Remove(key);
        }

        public void SetTimeScale(float value)
        {
            _timeScale = value;
        }

        public float TimeScale => _timeScale * (referenceKeeper ? referenceKeeper.TimeScale : 1);
        public float DeltaTime => Time.deltaTime * _timeScale;

        public WaitInstruction Wait(float time)
        {
            return new WaitInstruction(time, () => TimeScale);
        }
    }

    public interface ITimeInstance
    {
        void SetTimeScale(float value);
        float TimeScale { get; }
        float DeltaTime { get; }

        WaitInstruction Wait(float time);
    }
}