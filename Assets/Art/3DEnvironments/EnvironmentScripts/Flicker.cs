using System.Collections;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public float maxRange = 11.0f;
    public float minRange = 9.0f;
    public float changeRate = 0.1f;

    private Light lightComponent; // Cached component

    private void Awake()
    {
        lightComponent = GetComponent<Light>();
        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float previousLightRange = lightComponent.range;
            float nextLightRange = Random.Range(minRange, maxRange);
            float timer = 0.0f;
            while (timer < changeRate)
            {
                lightComponent.range = Mathf.Lerp(previousLightRange, nextLightRange, timer / changeRate);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
