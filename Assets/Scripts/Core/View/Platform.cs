using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Renderer _renderer;

    public float GetPlatformWidth()
    {
        return _renderer.bounds.size.x;
    }
    public Transform GetSpawnPoint(int noteType)
    {
        if (noteType == 1 || noteType == 2)
            return _spawnPoints[0];
        if (noteType == 3 || noteType == 4)
            return _spawnPoints[1];
        return _spawnPoints[2];
    }
}
