using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Effects
{
    public class Twinkling : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer[] _spriteRenderers;
        [SerializeField]
        private TMP_Text[] _tmpTexts;
        private Coroutine _twinklingCoroutine;

        public void StartTwinkling(float frequency)
        {
            _twinklingCoroutine = StartCoroutine(TwinklingEffect(frequency));
        }

        public void StopTwinkling()
        {
            StopCoroutine(_twinklingCoroutine);
            foreach (var sr in _spriteRenderers)
            {
                Color colorBefore = sr.color;
                sr.color = new Color(
                    r: colorBefore.r,
                    g: colorBefore.g,
                    b: colorBefore.b,
                    a: 1
                ); 
            }
            foreach (var tmpt in _tmpTexts)
            {
                Color colorBefore = tmpt.color;
                tmpt.color = new Color(
                    r: colorBefore.r,
                    g: colorBefore.g,
                    b: colorBefore.b,
                    a: 1
                ); 
            }
        }

        private IEnumerator TwinklingEffect(float frequency)
        {
            float elapsedTime = 0f;
            while (true)
            {
                elapsedTime += Time.deltaTime;
                var twinklingValue = (Math.Cos(elapsedTime * frequency - Math.PI) + 1) / 2f;

                foreach (var sr in _spriteRenderers)
                {
                    Color colorBefore = sr.color;
                    sr.color = new Color(
                        r: colorBefore.r,
                        g: colorBefore.g,
                        b: colorBefore.b,
                        a: (float)twinklingValue
                    ); 
                }
                foreach (var tmpt in _tmpTexts)
                {
                    Color colorBefore = tmpt.color;
                    tmpt.color = new Color(
                        r: colorBefore.r,
                        g: colorBefore.g,
                        b: colorBefore.b,
                        a: (float)twinklingValue
                    );
                }
                yield return null;
            }
        }
    }
}