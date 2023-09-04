using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kultie.GameMechanics.RPGSystem
{
    public abstract partial class Stat
    {
        public delegate void OnStatUpdate(float oldValue, float newValue);

        public abstract Key StatKey { get; }

        private readonly float _baseValue;
        private readonly List<StatModifier> _statModifiers;
        public event OnStatUpdate OnUpdate;

        public Stat(float baseValue)
        {
            _baseValue = baseValue;
            _statModifiers = new List<StatModifier>();
            UpdateValue();
        }

        protected float _value;

        /// <summary>
        /// Logic value of stats use for combat or functionality calculation
        /// </summary>
        public float Value => GetValue(_value);

        public float RawValue => _value;

        public void AddModifier(StatModifier mod)
        {
            _statModifiers.Add(mod);
            mod.SetContext(this);
            UpdateValue();
        }

        public bool RemoveModifier(StatModifier mod)
        {
            mod.Dispose();
            var result = _statModifiers.Remove(mod);
            UpdateValue();
            return result;
        }

        public bool HasModifier(StatModifier mod)
        {
            return _statModifiers.Contains(mod);
        }

        protected virtual float CalculateFinalValue()
        {
            float totalFlat = 0;
            float totalMult = 0;
            foreach (var mod in _statModifiers)
            {
                totalFlat += mod.FlatValue;
                totalMult += mod.MultValue;
            }

            var value = (_baseValue + totalFlat) * (1 + totalMult / 100);
            return (float)Math.Round(value, 2);
        }

        public void UpdateValue()
        {
            var oldValue = _value;
            var newValue = CalculateFinalValue();
            _value = newValue;
            OnUpdate?.Invoke(oldValue, newValue);
        }

        /// <summary>
        /// Use with caution, if this is used all modifier will be ignored
        /// Next call of UpdateValue() and or Add/Remove modifier will recalculate the value with modifiers
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(float value)
        {
            var oldValue = _value;
            _value = value;
            OnUpdate?.Invoke(oldValue, _value);
        }

        protected abstract float GetValue(float rawValue);

        public string GetValueString()
        {
            return $"{Value} - ({RawValue})";
        }
    }
}