using UnityEngine;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private RectTransform _target; 
    [SerializeField] private float _rotationSpeed = 100f; 
    [SerializeField] private float _frequency = 1f;
    private float _time;

    void Update()
    {
        if (_target == null)
            return;
        _time += Time.deltaTime;
        float rotationSpeed = _rotationSpeed * Mathf.Abs(Mathf.Sin(_time * _frequency * Mathf.PI));
        _target.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
