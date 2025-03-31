using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();
    private IObjectFactory<T> _factory;

    public ObjectPool(IObjectFactory<T> factory)
    {
        _factory = factory;
    }
    public void SetUpPool(int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }
    private void AddObjectToPool()
    {
        T obj = _factory.Create();
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public T Get()
    {
        if (_pool.Count == 0)
        {
            AddObjectToPool();
        }

        T obj = _pool.Dequeue();
        obj.gameObject.SetActive(true); 
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false); 
        _pool.Enqueue(obj);
    }
    public IEnumerable<T> GetAll()
    {
        return _pool;
    }
    public int Count => _pool.Count;
}
