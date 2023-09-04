using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kultie.GameMechanics.RPGSystem
{
    public partial class Stat
    {
        public enum Key
        {
            /// <summary>
            /// Flat and Mult modifier allow
            /// </summary>
            Attack,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            Maxhp,

            /// <summary>
            /// Flat and Mult modifier allow
            /// </summary>
            Speed,

            /// <summary>
            /// Mult modifier only
            /// </summary>
            PhysicalAmp,

            /// <summary>
            /// Mult modifier only
            /// </summary>
            MagicalAmp,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            ActionSpeed,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            JustGuardWindow,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            GuardDefend,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            DamageReduction,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            CriticalRate,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            CriticalDamage,

            /// <summary>
            /// Flat modifier only
            /// </summary>
            Cooldown,
        }

        public class Attack : Stat
        {
            public Attack(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.Attack;

            protected override float GetValue(float rawValue)
            {
                return rawValue;
            }
        }

        public class MaxHp : Stat
        {
            public MaxHp(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.Maxhp;

            protected override float GetValue(float rawValue)
            {
                return rawValue;
            }
        }

        public class ActionSpeed : Stat
        {
            public ActionSpeed(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.Speed;

            protected override float GetValue(float rawValue)
            {
                return 5 * (1 + (5 * rawValue) / (5 + 5 * Mathf.Abs(rawValue)));
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }

        public class PhysicalAmp : Stat
        {
            public PhysicalAmp(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.PhysicalAmp;

            protected override float GetValue(float rawValue)
            {
                return rawValue;
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.MultValue;
                }

                var value = _baseValue * (1 + bonus / 100);
                return (float)Math.Round(value, 2);
            }
        }

        public class MagicalAmp : Stat
        {
            public MagicalAmp(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.MagicalAmp;

            protected override float GetValue(float rawValue)
            {
                return rawValue;
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.MultValue;
                }

                var value = _baseValue * (1 + bonus / 100);
                return (float)Math.Round(value, 2);
            }
        }

        public class MotionSpeed : Stat
        {
            public MotionSpeed(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.ActionSpeed;

            protected override float GetValue(float rawValue)
            {
                return 1 + (5 * rawValue) / (5 + 5 * Mathf.Abs(rawValue));
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }

        public class JustGuardWindow : Stat
        {
            public JustGuardWindow(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.JustGuardWindow;

            protected override float GetValue(float rawValue)
            {
                return rawValue;
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }

        public class GuardDefend : Stat
        {
            public GuardDefend(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.GuardDefend;

            protected override float GetValue(float rawValue)
            {
                return 1 - (0.06f * rawValue / (1 + 0.06f * Mathf.Abs(rawValue)));
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }

        public class DamageReduction : Stat
        {
            public DamageReduction(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.DamageReduction;

            protected override float GetValue(float rawValue)
            {
                return 1 - (0.06f * rawValue / (1 + 0.06f * Mathf.Abs(rawValue)));
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }

        public class CritRate : Stat
        {
            public CritRate(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.CriticalRate;

            protected override float GetValue(float rawValue)
            {
                return rawValue;
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }

        public class CritDamage : Stat
        {
            public CritDamage(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.CriticalDamage;

            protected override float GetValue(float rawValue)
            {
                return rawValue;
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }

        public class CoolDown : Stat
        {
            public CoolDown(float baseValue) : base(baseValue)
            {
            }

            public override Key StatKey => Key.Cooldown;

            protected override float GetValue(float rawValue)
            {
                return 1 - (0.06f * rawValue / (1 + 0.06f * Mathf.Abs(rawValue)));
            }

            protected override float CalculateFinalValue()
            {
                float bonus = 0;
                foreach (var mod in _statModifiers)
                {
                    bonus += mod.FlatValue;
                }

                var value = _baseValue + bonus;
                return (float)Math.Round(value, 2);
            }
        }
    }
}