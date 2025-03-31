using System.Collections.Generic;
using System.Linq;
public class NotesPlacer
{
    private const float TACK_LENGTH_MULTIPLIER = 0.66f;
    private readonly int[] _notePositions;
    private readonly int _tactLength;
    private int _index;
    private ObjectPool<Note> _notePool;  
    private Dictionary<Platform, List<Note>> _activeBonuses = new Dictionary<Platform, List<Note>>();
    public int PlatfromsWithNotes { get; private set; }
    public NotesPlacer(ObjectPool<Note> notePool, int tactLength, List<int> notePositions)
    {
        _notePool = notePool;
        int notesAmount = CurrentSongInfoSingleton.Instance.NotesAmount; 
        _notePositions = notePositions.Take(notesAmount).ToArray();
        _tactLength = tactLength;
        PlatfromsWithNotes = (_notePositions.Length - _index) / _tactLength;
    }

    public void PlaceBonuses(List<Platform> platforms)
    {
        foreach (var platform in platforms)
        {
            
            float step = platform.GetPlatformWidth() / _tactLength;
            for (int i = 0; i < _tactLength; i++)
            {
                if (_index >= _notePositions.Length)
                    break;
                float xPosition = (i) * step; 
                UnityEngine.Vector3 spawnPoint = platform.GetSpawnPoint(_notePositions[_index]).position + new UnityEngine.Vector3(xPosition, 0, 0);
               
                Note bonus = _notePool.Get();
                bonus.transform.position = spawnPoint;
                bonus.transform.SetParent(platform.transform);
                bonus.DisactivateTraps();
                switch(UnityEngine.Random.Range(0, 9))
                {
                    case 1:
                        bonus.ActivateTrap(true);
                        break;
                    case 2:
                        bonus.ActivateTrap(false);
                        break;
                }

                if (!_activeBonuses.ContainsKey(platform))
                    _activeBonuses[platform] = new List<Note>();

                _activeBonuses[platform].Add(bonus);
                _index++;
                PlatfromsWithNotes = (_notePositions.Length - _index) / _tactLength;
            }
        }
    }

    public void ClearBonuses(Platform platform)
    {
        if (!_activeBonuses.ContainsKey(platform)) return;

        foreach (var bonus in _activeBonuses[platform])
        {
            _notePool.Return(bonus);
        }
        _activeBonuses[platform].Clear();
    }
    public bool HasNotes(Platform platform)
    {
        return _activeBonuses.ContainsKey(platform) && _activeBonuses[platform].Count > 0;
    }
}
