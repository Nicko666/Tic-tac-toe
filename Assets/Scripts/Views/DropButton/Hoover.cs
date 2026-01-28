using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoover : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AnimatorPresenter _animator;
    private bool _isHoover;

    public Action<bool> onEnter;
    public Action onHoover;
    public Action onDrop;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHoover = true;
        onEnter?.Invoke(_isHoover);
        _animator.Output(_isHoover);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _isHoover = false;
        onEnter?.Invoke(_isHoover);
        _animator.Output(_isHoover);
    }
    public void OnDrop(PointerEventData eventData) =>
        onDrop?.Invoke();

    private void Start() =>
        _animator.Output(_isHoover);

    private void Update()
    {
        if (_isHoover)
            onHoover?.Invoke();
    }

    public void OutputInteractable(bool value) =>
        gameObject.SetActive(value);

    [Serializable]
    class AnimatorPresenter
    {
        [SerializeField] private Animator _animator;
        private static readonly int AnimatorHooverBool = Animator.StringToHash("Hoover");

        internal void Output(bool value) =>
            _animator.SetBool(AnimatorHooverBool, value);
    }
}