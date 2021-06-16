using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] FadeInOut _panelRestart;
    [SerializeField] LoadingScreen _loadingScreen;
    [SerializeField] Game _game;

    public void Restart()
    {
        _panelRestart.gameObject.SetActive(false);
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.FadeInImage(_game);
    }
}
