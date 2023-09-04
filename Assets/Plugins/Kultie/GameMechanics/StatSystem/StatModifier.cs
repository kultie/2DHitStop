using System;
using UnityEngine;

namespace Kultie.GameMechanics.RPGSystem
{
    public class StatModifier
    {
        private float _flatValue;
        private float _multValue;

        /// <summary>
        /// The addition value of this modifier
        /// The stat that contain this value will automatically update when this value change
        /// </summary>
        public float FlatValue
        {
            set
            {
                var oldValue = _flatValue;
                if (!(Math.Abs(value - oldValue) > Mathf.Epsilon)) return;
                _flatValue = value;
                _context?.UpdateValue();
            }

            get => _flatValue;
        }
        /// <summary>
        /// The multiply value of this modifier
        /// The stat that contain this value will automatically update when this value change
        /// </summary>
        public float MultValue
        {
            set
            {
                var oldValue = _multValue;
                if (!(Math.Abs(value - oldValue) > Mathf.Epsilon)) return;
                _multValue = value;
                _context?.UpdateValue();
            }
            get => _multValue;
        }

        public IStatModifierSource Source;
        private Stat _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flatValue"></param>
        /// <param name="multValue"> Enter value between 0~100 as real percent value Example: 20 = 20%</param>
        /// <param name="source"></param>
        public StatModifier(float flatValue, float multValue, IStatModifierSource source)
        {
            _flatValue = flatValue;
            _multValue = multValue;
            Source = source;
        }
        /// <summary>
        /// When add to a stat call this function so that when the Mult or Flat value change stat's value will change
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(Stat context)
        {
            _context = context;
        }

        
        /// <summary>
        /// Call this function when this modifier has been remove from a stat's modifier collection
        /// </summary>
        /// <param name="context"></param>
        public void Dispose()
        {
            _context = null;
        }

        [Serializable]
        public abstract class SerializedModifier
        {
            protected StatModifier _mod;

            public abstract bool IsValid();

            public abstract StatModifier GetModifier(IStatModifierSource source);
        }

        [Serializable]
        public class FlatModifier : SerializedModifier
        {
            [SerializeField] private float flatValue;
            public IStatModifierSource Source;

            public override bool IsValid()
            {
                return flatValue != 0;
            }

            public override StatModifier GetModifier(IStatModifierSource source)
            {
                if (_mod == null)
                {
                    _mod = new StatModifier(flatValue, 0, source);
                }

                return _mod;
            }

            public override string ToString()
            {
                string sign = flatValue > 0 ? "+" : "-";
                return $"{sign}{flatValue}";
            }
        }

        [Serializable]
        public class MultModifier : SerializedModifier
        {
            [Tooltip("Enter value between 0~100 as real percent value Example: 20 = 20%")] [SerializeField]
            private float multValue;

            public override bool IsValid()
            {
                return multValue != 0;
            }

            public override StatModifier GetModifier(IStatModifierSource source)
            {
                if (_mod == null)
                {
                    _mod = new StatModifier(0, multValue, source);
                }

                return _mod;
            }

            public override string ToString()
            {
                string sign = multValue > 0 ? "+" : "-";
                return $"{sign}{multValue}%";
            }
        }

        [Serializable]
        public class MixModifier : SerializedModifier
        {
            [Tooltip("Enter value between 0~100 as real percent value Example: 20 = 20%")] [SerializeField]
            private float multValue;

            [SerializeField] private float flatValue;

            public override bool IsValid()
            {
                return multValue != 0 || flatValue != 0;
            }

            public override StatModifier GetModifier(IStatModifierSource source)
            {
                if (_mod == null)
                {
                    _mod = new StatModifier(flatValue, multValue, source);
                }

                return _mod;
            }

            public override string ToString()
            {
                string multSign = multValue > 0 ? "+" : "-";
                string flatSign = flatValue > 0 ? "+" : "-";
                return $"{flatSign}{flatValue}\n{multSign}{multValue}%";
            }
        }
    }
}