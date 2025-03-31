using UnityEngine;

public interface IObjectPool<T> where T : MonoBehaviour 
{
    T Get();
    void Return(T obj);
}