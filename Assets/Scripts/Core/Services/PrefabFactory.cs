using UnityEngine;
using Zenject;

public class PrefabFactory<T> : IObjectFactory<T> where T : MonoBehaviour
{
    private T _prefab;
    private DiContainer _container;

    public PrefabFactory(T prefab, DiContainer container)
    {
        _prefab = prefab;
        _container = container;
    }

    public T Create()
    {
        GameObject gameObject = _container.InstantiatePrefab(_prefab.gameObject);
        T obj = gameObject.GetComponent<T>();
        return obj;
    }
}
