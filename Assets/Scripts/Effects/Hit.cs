using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace Effects
{
    [Serializable]
    public class Hit
    {
        [SerializeField]
        private float _duration;
        [SerializeField]
        private SpriteRenderer[] _spriteRenderers;
        [SerializeField]
        private Color _fromColor;
        [SerializeField]
        private Color _toColor;

        public IEnumerator StartEffect()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                double strenght = Math.Sin(elapsedTime / _duration * Math.PI);
                
                yield return null;

                foreach (var _spriteRenderer in _spriteRenderers)
                {
                    _spriteRenderer.color = Color.Lerp(_fromColor, _toColor, (float)strenght);
                }
            }
        }

        public void SetLerp(float lerp)
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = Color.Lerp(_fromColor, _toColor, lerp);
            }
        }

        public void setVisible(bool isVisible)
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.enabled = isVisible;
            }
        }
    }
}