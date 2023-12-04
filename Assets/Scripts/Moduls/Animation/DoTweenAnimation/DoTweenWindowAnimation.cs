using DG.Tweening;
using UnityEngine;

public class DoTweenWindowAnimation : WindowAnimation
{
    [SerializeField] RectTransform _rectTransform;

    protected override void DisableAnimation()
    {
        _rectTransform.localScale = Vector3.one;
        _rectTransform.DOScale(1.0f, 0.1f).WaitForCompletion();

    }

    protected override void EnableAnimation()
    {
        _rectTransform.localScale = Vector3.zero;
        _rectTransform.DOScale(1.0f, 0.2f);

    }


}
