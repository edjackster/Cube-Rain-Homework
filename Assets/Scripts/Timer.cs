using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cube))]
public class Timer : MonoBehaviour
{
    public event UnityAction TimerUp;

    [SerializeField] private float _minLifeTime = 2;
    [SerializeField] private float _maxLifeTime = 5;

    public void StartCountdown()
    {
        float lifeTime = Random.Range(_minLifeTime, _maxLifeTime);

        Invoke(nameof(Disable), lifeTime);
    }

    private void Disable()
    {
        TimerUp?.Invoke();
    }
}
