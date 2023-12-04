using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class RTDrag : RTClick, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    Transform _parent;
    Canvas _canvas;
    CanvasGroup _canvasGroup;
    bool _isDragging = false;
    public Vector2 _position;

    public Action<RT, RT> onEnterRT;


    private void Awake()
    {
        _canvas = FindAnyObjectByType<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = true;

    }

    public override Vector2 LocalPosition 
    {
        set
        {
            if (!_isDragging) base.LocalPosition = value;
            _position = value;
        }
    }

    public override Transform SetParent
    {
        set
        {
            _parent = value;
            base.SetParent = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;

        RectTransform.SetParent(_canvas.transform);

        _canvasGroup.blocksRaycasts = false;
    
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform.anchoredPosition += (eventData.delta / _canvas.scaleFactor) * Vector2.up;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        SetParent = _parent;

        LocalPosition = _position;

        _canvasGroup.blocksRaycasts = true;
    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var enterObject = eventData.pointerDrag;

        RT newRT;

        if (enterObject != null && enterObject.TryGetComponent<RT>(out newRT))
        {
            onEnterRT?.Invoke(this, newRT);
        }

    }


}
