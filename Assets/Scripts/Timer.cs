using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cube))]
public class Timer : MonoBehaviour
{
    [SerializeField] private float _minTime = 2;
    [SerializeField] private float _maxTime = 5;

    public event UnityAction TimesUp;

    public void StartCountdown()
    {
        float time = Random.Range(_minTime, _maxTime);

        StartCoroutine(nameof(Countdown), time);
    }

    private IEnumerator Countdown(int delay)
    {
        yield return new WaitForSeconds(delay);

        TimesUp?.Invoke();
    }
}
