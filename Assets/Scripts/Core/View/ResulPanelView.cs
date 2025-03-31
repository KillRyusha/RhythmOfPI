using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResulPanelView : MonoBehaviour
{
    private const int MENU_INDEX = 0;
    private const int GAME_SCENE_INDEX = 1;
    [SerializeField] private TMP_Text _resulText;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _goToMenuButton;

    private void Awake()
    {
        _goToMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(MENU_INDEX); });
        _restartButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
    }
    public void ShowResult(string result)
    {
        _resulText.text = "Your score: " + result;
        _resultPanel?.SetActive(true);
    }

}