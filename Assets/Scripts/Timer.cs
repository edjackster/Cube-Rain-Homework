using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Cube))]
public class Timer : MonoBehaviour
{
    [SerializeField] private float _minTime = 2;
    [SerializeField] private float _maxTime = 5;

    public event Action TimesUp;

    public void StartCountdown()
    {
        float time = UnityEngine.Random.Range(_minTime, _maxTime);

        StartCoroutine(Countdown(time));
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSeconds(delay);

        TimesUp?.Invoke();
    }
}
