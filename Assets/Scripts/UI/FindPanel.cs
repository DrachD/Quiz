using System;
using UnityEngine;
using UnityEngine.UI;

public class FindPanel : MonoBehaviour
{
    public Action<string> OnUpdateFindTextEvent;

    private FadeIn _fadeIn;

    [SerializeField] Text text;

    private void Awake()
    {
        _fadeIn = text.GetComponent<FadeIn>();
        OnUpdateFindTextEvent += _fadeIn.UpdateText;
    }
}
