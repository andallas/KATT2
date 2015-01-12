using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public float duration;
    private float magnitude = 0.125f;

    public void CameraShake()
    {
        StartCoroutine(Shake(duration));
    }

    public void CameraShake(float seconds)
    {
        StartCoroutine(Shake(seconds));
    }

    IEnumerator Shake(float seconds)
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < seconds)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / seconds;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;

            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }
}