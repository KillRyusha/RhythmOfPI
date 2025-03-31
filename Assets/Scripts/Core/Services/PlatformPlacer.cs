using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformPlacer : ACompletableService
{
    private NotesPlacer _bonusManager;
    private GameSpeedService _speed;
    private ObjectPool<Platform> _platformPool;
    private List<Platform> _activePlatforms = new List<Platform>();
    private bool _allNotesCollected = false;
    private Action _onServiceComplete;
    private LastPlatfrom _lastPlatform;

    public PlatformPlacer(ObjectPool<Platform> platformPool, NotesPlacer bonusManager, GameSpeedService speed, LastPlatfrom lastPlatfrom)
    {
        _platformPool = platformPool;
        _bonusManager = bonusManager;
        _speed = speed;
        _lastPlatform = lastPlatfrom;
    }

    public void Initialize()
    {
        int totalPlatforms = _platformPool.Count;
        for (int i = 0; i < totalPlatforms; i++)
        {
            Platform platform = _platformPool.Get();
            platform.transform.position = new Vector3(i * platform.GetPlatformWidth(), 0, 0);
            _activePlatforms.Add(platform);
        }

        _bonusManager.PlaceBonuses(_activePlatforms.Skip(1).ToList());
        _lastPlatform.gameObject.SetActive(false);
    }

    public void Update()
    {
        MoveFloor();
    }

    private void MoveFloor()
    {
        foreach (var platform in _activePlatforms)
        {
            platform.transform.position += Vector3.left * _speed.Speed * Time.deltaTime;
        }

        if (_speed.Speed <= 0 && !_serviceCompleted && _allNotesCollected)
        {
            OnComplete();
        }

        Platform firstSegment = _activePlatforms?.First();
        if (firstSegment.transform.position.x < Camera.main.transform.position.x - firstSegment.GetPlatformWidth() / 2)
        {
            RepositionFloor();
        }
    }

    private void RepositionFloor()
    {
        Platform firstSegment = _activePlatforms.First();
        _bonusManager.ClearBonuses(firstSegment);
        _platformPool.Return(firstSegment);

        if (!_allNotesCollected)
        {
            Platform lastSegment = _activePlatforms.Last();
            firstSegment.transform.position = lastSegment.transform.position + Vector3.right * firstSegment.GetPlatformWidth();

            _activePlatforms.RemoveAt(0);
            _activePlatforms.Add(firstSegment);
            firstSegment.gameObject.SetActive(true);
            _bonusManager.PlaceBonuses(new List<Platform> { firstSegment });

            int remainingPlatformsWithNotes = _bonusManager.PlatfromsWithNotes;
            if (remainingPlatformsWithNotes <= 1)
            {
                _allNotesCollected = true;
            }
        }
        else
        {
            if (!_lastPlatform.gameObject.activeSelf)
            {
                _lastPlatform.gameObject.SetActive(true);
                _lastPlatform.transform.position = _activePlatforms.Last().transform.position + Vector3.right * (_lastPlatform.GetPlatformWidth() *2);
                _activePlatforms.Add(_lastPlatform);
            }

            _activePlatforms.RemoveAt(0);
            firstSegment.gameObject.SetActive(false);
        }
    }
}
