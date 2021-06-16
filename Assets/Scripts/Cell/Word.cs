using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Word : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public Sprite Icon
    {
        set => _spriteRenderer.sprite = value;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private Sequence _seq;

    /// <summary>
    /// activation of the effect bouns effect
    /// </summary>
    public void BounceEffect()
    {
        Sequence seq = DOTween.Sequence();
        _seq = seq;

        seq.Append(transform.DOLocalMoveY(.1f, .2f))
        .Append(transform.DOLocalMoveY(-.1f, .2f))
        .Append(transform.DOLocalMoveY(.2f, .2f))
        .Append(transform.DOLocalMoveY(-.2f, .2f))
        .Append(transform.DOLocalMoveY(.5f, .1f))
        .Append(transform.DOLocalMoveY(-.5f, .1f))
        .Append(transform.DOLocalMoveY(0f, .1f));
    }

    /// <summary>
    /// when you turn on the object, we reset the coordinates
    /// </summary>
    private void OnEnable()
    {
        transform.localPosition = Vector2.zero;
    }

    /// <summary>
    /// kill all animation when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        _seq.Kill();
    }
}
