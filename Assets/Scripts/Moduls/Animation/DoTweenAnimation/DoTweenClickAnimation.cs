using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

//[RequireComponent(typeof(RectTransform), typeof (CanvasGroup))]
public class DoTweenClickAnimation : ClickAnimation
{
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] Button _button;

    [SerializeField] bool _scaleIn;
    [SerializeField] float _speed;


    private void Start()
    {
        _button.onClick.AddListener(Click);
    }

    public DoTweenClickAnimation()
    {
        _speed = 0.1f;
    }

    public override void Click()
    {
        if (_button.interactable == true)
        {
            _rectTransform?.DOScale(_scaleIn ? 0.5f : 1.5f, _speed);
            _rectTransform?.DOScale(1.0f, _speed).SetDelay(_speed);
        }

    }

    public override void InteractibleAnimation(bool value)
    {
        //_canvasGroup.alpha = value ? 1.0f : 0.5f;

    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Click);
    }


}
