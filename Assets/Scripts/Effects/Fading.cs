using System.Collections;
using TMPro;
using UnityEngine;

namespace Effects
{
    public class Fading : MonoBehaviour
    {
        [SerializeField]
        private float _duration;
        [SerializeField]
        private AnimationCurve _curve;
        [SerializeField]
        private TextMeshProUGUI _text;

        public void startFade()
        {
            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                float strenght = _curve.Evaluate(elapsedTime / _duration);
                var c = _text.color;
                _text.color = new Color(c.r, c.g, c.b, strenght);
                yield return null;
            }
        }
    }
}
