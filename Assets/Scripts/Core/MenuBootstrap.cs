using UnityEngine;

public class MenuBootstrap : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private GameObject _loadingScreen;

    private void Awake()
    {
        _loadingScreen.SetActive(true);
    }

    private void DiactivateLoadingScreen(string response)
    {
        _loadingScreen.SetActive(false);
    }

}
