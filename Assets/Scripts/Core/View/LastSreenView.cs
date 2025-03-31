using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastSreenView : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 10f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _dir;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Image _mainImage;

    private void Start()
    {
        StartCoroutine(TryGetSprite());   
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position+ _offset,  _dir, out hit, _raycastDistance, _layerMask))
        {
            if (hit.collider.CompareTag("Finish"))
            {
                transform.SetParent(hit.transform);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + _offset,  _dir * _raycastDistance);
    }

    private IEnumerator TryGetSprite()
    {
        while (CurrentSongInfoSingleton.Instance.SongSptite == null) 
            yield return new WaitForSeconds(0.2f);
        _mainImage.sprite = CurrentSongInfoSingleton.Instance.SongSptite;
    }
}
