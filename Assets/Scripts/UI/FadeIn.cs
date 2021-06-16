using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] string _baseText;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void UpdateText(string value)
    {
        text.text = _baseText + value;
        FadeInText();
    }

    private void FadeInText()
    {
        text.color = new Color(1f, 1f, 1f, 0f);
        DOVirtual.Float(0f, 1f, 2f, (c) => text.color = new Color(1f, 1f, 1f, c));
    }
}
