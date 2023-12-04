using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RT : MonoBehaviour
{
    RectTransform _rectTransform;
    protected RectTransform RectTransform => (_rectTransform ??= GetComponent<RectTransform>());

    bool _doAnimation = false;
    float _animationSpeed = 0.15f;


    private void Start()
    {
        StartCoroutine(AllowAnimation()); 

        IEnumerator AllowAnimation()
        {
            yield return new WaitForEndOfFrame();
            _doAnimation = true;
        }
    }
    
    virtual public Vector2 LocalPosition
    {
        set
        {
            if (!_doAnimation)
                RectTransform.localPosition = value;
            else
            {
                RectTransform.DOLocalMove(value, _doAnimation ? _animationSpeed : 0f);
            }
        }
    }

    virtual public Vector2 SizeDelta
    {
        set
        {
            if (!_doAnimation)
                RectTransform.sizeDelta = value;
            else
            {
                RectTransform.DOSizeDelta(value, _doAnimation ? _animationSpeed : 0f);
            }
        }
    }
    
    virtual public Vector2 LocalScale
    {
        set => RectTransform.localScale = value;
    }

    virtual public Transform SetParent
    {
        set => transform.SetParent(value);
    }

    public virtual void HiddenMove(Vector2 position)
    {
        Vector3 scale = RectTransform.transform.localScale;
        RectTransform.DOScale(Vector3.zero, _animationSpeed / 2);
        RectTransform.DOLocalMove(position, 0f).SetDelay(_animationSpeed/2);
        RectTransform.DOScale(Vector3.one, _animationSpeed / 2).SetDelay(_animationSpeed/2);
    }


}
