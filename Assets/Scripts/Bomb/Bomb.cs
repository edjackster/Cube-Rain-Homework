using System;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private AlphaChanger _alphaChanger;
    
    private Timer _timer;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _timer.TimesUp += Explode;
    }

    private void OnDisable()
    {
        _timer.TimesUp -= Explode;
    }

    public void StartTimer()
    {
        _timer.StartCountdown();
    }

    public void RollBack()
    {
        _alphaChanger.RollBack();
    }

    private void Explode()
    {   
        _exploder.Explode(gameObject);
        Exploded?.Invoke(this);
    }
}
