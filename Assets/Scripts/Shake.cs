using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    public void StartShake(float maxOffset, float duration)
    {
        StopShake();
        StartCoroutine(ShakeSequence(maxOffset, duration));
    }

    public void StopShake()
    {
        StopAllCoroutines();
        transform.localPosition = initialPosition;
    }

    private IEnumerator ShakeSequence(float offset, float duration)
    {
        float durationPassed = 0;
        while (durationPassed < duration)
        {
            DoShake(offset);
            durationPassed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
    }

    private void DoShake(float maxOffset)
    {
        float xOffset = Random.Range(-maxOffset, maxOffset);
        float yOffset = Random.Range(-maxOffset, maxOffset);
        transform.localPosition = new Vector3(initialPosition.x + xOffset, initialPosition.y + yOffset, initialPosition.z);
    }
}
