using System;
using System.Collections;
using UnityEngine;

namespace Effects
{
    public class Hit : MonoBehaviour
    {
        [SerializeField] 
        private AnimationCurve _animationCurve;

        [SerializeField] 
        private float _duration;

        [SerializeField]
        private SpriteRenderer[] _spriteRenderers;
        

        public void StartEffect()
        {
            StartCoroutine(IEStartEffect());
        }

        public void MarkAsDestroyed()
        {
            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = Color.Lerp(Color.white, Color.red, 0.7f);
            }
        }

        IEnumerator IEStartEffect()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                float strenght = _animationCurve.Evaluate(elapsedTime / _duration);

                foreach (var _spriteRenderer in _spriteRenderers)
                {
                    _spriteRenderer.color = Color.Lerp(Color.white, Color.red, strenght);
                }
                
                yield return null;
            }
        }
    }
}