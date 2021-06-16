using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] FloatVariable _duration;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color(1f, 1f, 1f, 0f);
    }

    private void Start()
    {
        DOVirtual.Float(0f, 1f, _duration.value, (c) => image.color = new Color(0f, 0f, 0f, c));
    }
}
