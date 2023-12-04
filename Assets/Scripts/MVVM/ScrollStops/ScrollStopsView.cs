using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollStopsView : MonoBehaviour, IDropHandler
{
    float _enableTime;

    [SerializeField] RectTransform _rectTransform;
    [SerializeField] int _positionsNumber;

    [SerializeField] CanvasGroup[] _buttons;

    float _currentPosition => _rectTransform.localPosition.x;
    float _size => _rectTransform.sizeDelta.x;
    float _step => _size / _positionsNumber;
    int _nextPositionIndex
    {
        get
        {
            int index = Mathf.Abs(Mathf.RoundToInt(_currentPosition / _step));
            return Mathf.Clamp(index, 0, _positionsNumber);
        }
    }


    public Action<int> onPositionChanged;


    private void OnEnable()
    {
        _enableTime = Time.time;
        
    }

    private void Start()
    {
        SetPositionByIndex(_nextPositionIndex);
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        SetPositionByIndex(_nextPositionIndex);

    }

    public void SetPositionByIndex(int index)
    {
        var nextPosition = _step * -index;

        if (Time.time > _enableTime)
        {
            _rectTransform.DOLocalMoveX(nextPosition, 0.2f);
        }
        else
        {
            _rectTransform.localPosition = new Vector2(nextPosition, _rectTransform.localPosition.y);
        }
        
        onPositionChanged?.Invoke(index);

        HighliteButton(index);

    }

    void HighliteButton(int index)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].DOFade(i == index ? 1.0f : 0.5f, 0.1f);
        }

    }


}