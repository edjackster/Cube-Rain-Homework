using System;
using UnityEngine;

[RequireComponent(typeof(Timer), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;
    private Timer _timer;
    private bool _isHitted = false;

    public event Action<Cube> Died;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        _timer.TimesUp += OnTimerUp;
    }

    private void OnDisable()
    {
        _timer.TimesUp -= OnTimerUp;
    }

    public void RollBack()
    {
        _colorChanger.RollBack();
        _isHitted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out var wall) == false)
            return;

        if (_isHitted)
            return;

        _isHitted = true;
        _colorChanger.ChangeColor();
        _timer.StartCountdown();
    }

    private void OnTimerUp()
    {
        Died?.Invoke(this);
    }
}
