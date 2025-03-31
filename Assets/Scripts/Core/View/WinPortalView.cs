using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPortalView : MonoBehaviour
{
    [SerializeField] private NoteUIController _noteUIController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
           _noteUIController.ShowResultPanel();
    }
}
