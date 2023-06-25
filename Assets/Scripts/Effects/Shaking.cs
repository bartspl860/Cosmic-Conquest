using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _curve;

    public void startShake(float duration)
    {
        StartCoroutine(Shake(duration));
    }

    private IEnumerator Shake(float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            float strenght = _curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strenght;
            yield return null;
        }

        transform.position = startPosition;
    }
}
