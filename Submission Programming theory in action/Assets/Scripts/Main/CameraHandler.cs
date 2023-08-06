using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private bool isShaking = false;
    private Coroutine shakeCoroutine;

    public void OnEnable()
    {
        Factory.OnFactoryDestroyed += Shake;
    }

    public void OnDisable()
    {
        Factory.OnFactoryDestroyed -= Shake;
    }

    public void Shake()
    {
        if (isShaking) return;
        isShaking = true;
        StartCoroutine(ShakeCoroutine(0.2f, 0.1f));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        isShaking = false;
        transform.localPosition = originalPosition;
    }

    private void OnDestroy()
    {
        Factory.OnFactoryDestroyed -= Shake;
    }
}
