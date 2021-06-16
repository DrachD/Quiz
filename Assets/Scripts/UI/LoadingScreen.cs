using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] FloatVariable _duration;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color(1f, 1f, 1f, 0f);
    }

    /// <summary>
    /// when loading the screen, you must wait for the time at which the game can update the game field
    /// </summary>
    public void FadeInImage(Game game)
    {
        image.color = new Color(1f, 1f, 1f, 0f);
        DOVirtual.Float(0f, 1f, _duration.value, (c) => image.color = new Color(1f, 1f, 1f, c));

        // turn off the loading screen when our game is ready
        StartCoroutine(game.GameRestart(() => gameObject.SetActive(false), _duration.value));
    }
}
