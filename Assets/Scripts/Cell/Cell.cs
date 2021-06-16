using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    class MyStringEvent : UnityEvent<Action, string> {}
    private MyStringEvent _unityEvent = new MyStringEvent();

    [SerializeField] Word _word;

    private string _text;
    public string Text => _text;

    public void Init(Sprite icon, string text, Game game)
    {
        _word.Icon = icon;
        _text = text;

        _unityEvent.AddListener(game.MatchCheck);
    }

    /// <summary>
    /// activation of the effect easeInBounce effect
    /// </summary>
    public void StartAnimation()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(new Vector3(1, 1, 1), .2f));
        seq.Append(transform.DOScale(new Vector3(.8f, .8f, .8f), .3f));
        seq.Append(transform.DOScale(new Vector3(1, 1, 1), .2f));
    }

    /// <summary>
    /// when clicking with the right mouse button, we check for coincidence of values
    /// </summary>
    private void OnMouseDown()
    {
        _unityEvent.Invoke(() => _word.BounceEffect(), _text);
    }
}
