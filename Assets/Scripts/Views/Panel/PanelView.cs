using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelView : MonoBehaviour
{
    [SerializeField] private Button[] _closeButtons;
    [SerializeField] private Animator _animator;
    [SerializeField] private CanvasGroup _canvasGroup;
    private static readonly int OpenBool = Animator.StringToHash("Open");

    public event UnityAction onInputClose
    {
        add => Array.ForEach(_closeButtons, i => i.onClick.AddListener(value));
        remove => Array.ForEach(_closeButtons, i => i.onClick.RemoveListener(value));
    }

    public void OutputOpen(bool value)
    {
        if (_animator)
            _animator.SetBool(OpenBool, value);
        _canvasGroup.blocksRaycasts = value;
    }

    public void OutputInteractable(bool isInteractable) =>
        Array.ForEach(_closeButtons, i => i.interactable = isInteractable);
}