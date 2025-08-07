using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Timer), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> Die;

    private ColorChanger _colorChanger;
    private Timer _timer;
    
    private bool _isHitted = false;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        _timer.TimerUp += OnTimerUp;
    }

    private void OnDisable()
    {
        _timer.TimerUp -= OnTimerUp;
    }

    public void RollBack()
    {
        _colorChanger.RollBack();
        _isHitted = false;
    }

    public void OnPlatformCollisionEnter()
    {
        if (_isHitted)
            return;

        _isHitted = true;
        _colorChanger.ChangeColor();
        _timer.StartCountdown();
    }

    private void OnTimerUp()
    {
        Die?.Invoke(this);
    }
}
