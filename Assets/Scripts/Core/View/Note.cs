using UnityEngine;
using Zenject;

public class Note : MonoBehaviour
{
    [SerializeField] private GameObject _sharpObject;
    [SerializeField] private GameObject _flatObject;
    [SerializeField] private GameObject _noteObject;
    [Inject] private NoteUIController _noteUIController;

    public void DisactivateTraps()
    {
        _sharpObject.SetActive(false);
        _flatObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _noteUIController.ChangeCollectedNotesAmount();
            _noteObject.SetActive(false);
        }
    }
    public void ActivateTrap(bool sharpTrap)
    {
        _sharpObject.SetActive(sharpTrap);
        _flatObject.SetActive(!sharpTrap);
    }
}