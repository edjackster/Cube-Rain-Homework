using System.Collections;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private const float Tick = 0.01f;

    [SerializeField] private float _minTime = 2;
    [SerializeField] private float _maxTime = 5;

    public event Action TimesUp;
    public event Action<float> TimeChanged;

    public void StartCountdown()
    {
        float time = UnityEngine.Random.Range(_minTime, _maxTime);

        StartCoroutine(Countdown(time));
    }

    private IEnumerator Countdown(float delay)
    {
        var wait = new WaitForSeconds(Tick);
        int ticksCount = (int)(delay / Tick);

        for (int i = 0; i < ticksCount; i++)
        {
            yield return wait;
            TimeChanged?.Invoke(1 - (float)i / ticksCount);
        }

        TimesUp?.Invoke();
    }
}