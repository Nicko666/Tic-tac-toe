using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class PlayersListItemPresenter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _markImage, _logicImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _dragThreshold;
    private PlayerModel _player;
    private Coroutine _dragCoroutine;

    internal Action<PlayerModel> onInputClick;
    internal Action<PlayersListItemPresenter, bool> onInputHold;
    internal Action<PlayersListItemPresenter> onInputEnter;
    internal Action<PlayersListItemPresenter, PointerEventData> onInputBeginDrag;
    internal Action<PlayersListItemPresenter, PointerEventData> onInputDrag;
    internal Action<PlayersListItemPresenter, PointerEventData> onInputEndDrag;

    private static readonly int PressedBool = Animator.StringToHash("Pressed");
    private static readonly int DraggedBool = Animator.StringToHash("Dragged");

    internal void OutputPlayer(PlayerModel player)
    {
        _player = player;

        _markImage.sprite = _player.mark.Icon;
        _markImage.color = Color.HSVToRGB(_player.hue, _player.saturation, 1);
        _logicImage.sprite = _player.logic.Icon;
    }
    
    internal void OutputIsDraggin(bool isDragging) =>
        _animator.SetBool(DraggedBool, isDragging);

    internal void OutputDrag(Vector2 delta) =>
        _rectTransform.anchoredPosition += delta;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_dragCoroutine != null) StopCoroutine(_dragCoroutine);
        _dragCoroutine = null;
        onInputBeginDrag.Invoke(this, eventData);
    }
    public void OnDrag(PointerEventData eventData) =>
        onInputDrag.Invoke(this, eventData);
    public void OnEndDrag(PointerEventData eventData) =>
        onInputEndDrag.Invoke(this, eventData);

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.SetBool(PressedBool, true);

        if (_dragCoroutine != null) StopCoroutine(_dragCoroutine);
        _dragCoroutine = StartCoroutine(PointerDownAsync());
        IEnumerator PointerDownAsync()
        {
            yield return new WaitForSeconds(_dragThreshold);
            onInputHold.Invoke(this, true);
            _dragCoroutine = null;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _animator.SetBool(PressedBool, false);

        if (_dragCoroutine == null)
            onInputHold.Invoke(this, false);
        else
        {
            StopCoroutine(_dragCoroutine);
            onInputClick.Invoke(_player);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData) =>
        onInputEnter.Invoke(this);
}