using TMPro;
using UnityEngine;

public class NoteUIController : MonoBehaviour
{
    private const float TACK_LENGTH_MULTIPLIER = 0.66f;
    [SerializeField] private TMP_Text _notesText;
    [SerializeField] private ResulPanelView _resultPanel;
    private int _maxNotes = 10;
    private int _currentNotes = 0;

    private void Start()
    {
        _maxNotes = CurrentSongInfoSingleton.Instance.NotesAmount;
        _notesText.text = _currentNotes + "/" + _maxNotes;
    }
    public void ChangeCollectedNotesAmount()
    {
        _maxNotes = CurrentSongInfoSingleton.Instance.NotesAmount;
        _currentNotes += 1;
        _notesText.text = _currentNotes + "/" + _maxNotes;
    }
    public void ShowResultPanel() 
    {
        _resultPanel.ShowResult(_notesText.text);
    }

}
