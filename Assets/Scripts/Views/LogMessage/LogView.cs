using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class LogView : MonoBehaviour
{
    [SerializeField] private Button _openButton, _closeButton;
    [SerializeField] private GameObject _openPanel, _closePanel;
    [SerializeField] private ScrollRect _logScrollRect;
    [SerializeField] private LogShortView _logPrefab;
    [SerializeField] private LogDatabase _logDatabase;
    [SerializeField] private LogLongView _log;
    private PanelsController _panelsController = new();

    private readonly List<(LogModel model, LogShortView view)> _logs = new();

    private void Awake()
    {
        Application.logMessageReceived += OutputLog;
        _panelsController.onChanged += OutputPanel;
        _openButton.onClick.AddListener(_panelsController.SetOpen);
        _closeButton.onClick.AddListener(_panelsController.SetClose);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= OutputLog;
        _panelsController.onChanged -= OutputPanel;
        _openButton.onClick.RemoveListener(_panelsController.SetOpen);
        _closeButton.onClick.RemoveListener(_panelsController.SetClose);
    }
    private void Start()
    {
        _panelsController.SetClose();
    }

    private void OutputPanel(PanelModel panel)
    {
        _openPanel.SetActive(panel == PanelModel.Open);
        _closePanel.SetActive(panel == PanelModel.Close);
    }

    private void OutputLog(string condition, string stackTrace, LogType type)
    {
        LogModel log = new LogModel() { condition = condition, stackTrace = stackTrace, type = type };
        LogShortView view = Instantiate(_logPrefab, _logScrollRect.content);
        view.gameObject.SetActive(true);
        view.onInput += Input;
        view.Outputlog(log, _logDatabase);
        _logs.Add(new (log, view));
    }

    private void Input(LogShortView view)
    {
        LogModel log = _logs.Find(i => i.view == view).model;
        _logs.ForEach(i => i.view.OutputIsSelected(i.view == view));
        _log.OutputLog(log);
    }

    class PanelsController
    {
        internal Action<PanelModel> onChanged;
        internal void SetOpen() => onChanged.Invoke(PanelModel.Open);
        internal void SetClose() => onChanged.Invoke(PanelModel.Close);
    }

    enum PanelModel
    {
        Open,
        Close,
    }
}

internal struct LogModel
{
    internal string condition;
    internal string stackTrace;
    internal LogType type;
}

[Serializable]
internal struct LogDatabase
{
    public Color Error, Assert, Warning, Log, Exception;
}