using System;
using UnityEngine;

public class GameSpeedService
{
    public event Action<float> OnSpeedChanged;
    private float _speed = 0f;
    private float _targetSpeed;
    private float _changeDuration;
    private float _maxDuration = 20;
    private float _maxSlowDuration = 12;
    private float _maxSpeed = 20;
    private float _changeTimer;
    private bool _isChanging;
    public float Speed => _speed; 

    public void Update(float deltaTime)
    {
        if (!_isChanging) return;

        _changeTimer += deltaTime;
        float t = Mathf.Clamp01(_changeTimer / _changeDuration);
        _speed = Mathf.MoveTowards(_speed, _targetSpeed, t);

        OnSpeedChanged?.Invoke(_speed);

        if (_changeTimer >= _changeDuration)
        {
            _speed = _targetSpeed;
            _isChanging = false;
        }
    }

    public void IncreaseSpeed()
    {
        SetTargetSpeed(_maxSpeed, _maxDuration);
    }

    public void DecreaseSpeed()
    {
        SetTargetSpeed(0, _maxSlowDuration);
    }

    private void SetTargetSpeed(float newSpeed, float duration)
    {
        _targetSpeed = newSpeed;
        _changeDuration = duration;
        _changeTimer = 0f;
        _isChanging = true;
    }
}