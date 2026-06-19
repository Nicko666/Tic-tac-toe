using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class LogShortView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _isSelectedObject;
    private bool _isSelected;

    internal Action<LogShortView> onInput;

    internal void Outputlog(LogModel log, LogDatabase logDatabase)
    {
        _text.text = log.condition.ToString();

        _text.color = log.type switch
        {
            LogType.Error => logDatabase.Error,
            LogType.Assert => logDatabase.Assert,
            LogType.Warning => logDatabase.Warning,
            LogType.Log => logDatabase.Log,
            LogType.Exception => logDatabase.Exception,
            _ => logDatabase.Log,
        };
    }

    internal void OutputIsSelected(bool isSelected)
    {
        _isSelected = isSelected;
        _isSelectedObject.gameObject.SetActive(_isSelected);
    }

    private void Awake() =>
        _button.onClick.AddListener(Input);
    private void OnDestroy() =>
        _button.onClick.RemoveListener(Input);
    private void Start() =>
        _isSelectedObject.gameObject.SetActive(_isSelected);

    private void Input() =>
        onInput.Invoke(this);
}