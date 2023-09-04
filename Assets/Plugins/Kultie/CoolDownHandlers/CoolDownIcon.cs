using System;
using System.Collections;
using System.Collections.Generic;
using Kultie.GameMechanics.TimeSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Kultie.GameMechanics.CoolDownSystem
{
    public class CoolDownIcon : MonoBehaviour
    {
        [Required] [SerializeField] private Image icon;
        [Required] [SerializeField] private Image coolDownMask;
        [SerializeField] private Image counterMask;

        [Tooltip("Text use for displaying ability stack or cast streak")] [SerializeField]
        private TextMeshProUGUI numberText;

        [SerializeField] private GameObject effect;
        [SerializeField] private float effectTime = 0.5f;
        public Image Icon => icon;
        protected ICoolDown _coolDown;
        protected ICoolDownCounter _counter;
        private CoroutineReference _couroutineRef;

        public virtual void Setup(ICoolDown coolDownHandle, Sprite iconSprite)
        {
            _coolDown = coolDownHandle;
            _coolDown.OnReady += DisplayEffect;
            _counter = coolDownHandle.Counter;
            if (numberText)
            {
                numberText.text = string.Empty;
            }

            if (counterMask)
            {
                counterMask.fillAmount = 0.0f;
            }

            if (icon && iconSprite != null)
            {
                icon.sprite = iconSprite;
            }
        }

        private void Update()
        {
            if (_coolDown == null) return;
            coolDownMask.fillAmount = _coolDown.RemainPercent;
            if (_counter.RemainCounter > 1 && numberText)
            {
                numberText.text = $"{_counter.RemainCounter}";
            }
            else
            {
                numberText.text = string.Empty;
            }

            if (counterMask)
            {
                counterMask.fillAmount = _counter.RemainPercent;
            }
        }

        private void DisplayEffect()
        {
            if (!isActiveAndEnabled) return;
            if (!effect) return;
            _couroutineRef.Stop();
            _couroutineRef = this.Schedule(PlayEffectSequence());
        }

        IEnumerator PlayEffectSequence()
        {
            effect.SetActive(true);
            yield return TimeKeeper.Global.Wait(effectTime);
            effect.SetActive(false);
        }

        private void OnDisable()
        {
            _couroutineRef.Stop();
            if (effect)
            {
                effect.SetActive(false);
            }
        }
    }
}