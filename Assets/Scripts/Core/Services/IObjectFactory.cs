using UnityEngine;

public interface IObjectFactory<T> where T : MonoBehaviour
{
    T Create();
}
