using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private TogglesTextView _togglesText;
    [SerializeField] private Slider _volumeSlider;
    
    private SettingsModel _settings;
    private SettingsDatabase _appDatabase;

    public event UnityAction onInputClosePanel
    {
        add => _panel.onInputClose += value;
        remove => _panel.onInputClose -= value;
    }
    public Action<SettingsModel> onInputSettings;

    public void OutputDatabase(SettingsDatabase appDatabase)
    {
        _appDatabase = appDatabase;
        _togglesText.OutputToggles(Array.ConvertAll(_appDatabase.FrameIntervals, i => $"{i.FramesCount}p"));
    }

    public void OutputPanel(bool value) =>
        _panel.OutputOpen(value);

    public void OutputSettings(SettingsModel settings)
    {
        _settings = settings;
        _volumeSlider.SetValueWithoutNotify(_settings.volume);
        _togglesText.OutputIsToggled(Array.IndexOf(_appDatabase.FrameIntervals, settings.frameInteravl));
    }

    private void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(InputVolume);
        _togglesText.onInput += InputFPS;
    }

    private void InputVolume(float value)
    {
        _settings.volume = value;
        onInputSettings.Invoke(_settings);
    }

    private void InputFPS(int index)
    {
        _settings.frameInteravl = _appDatabase.FrameIntervals[index];
        onInputSettings.Invoke(_settings);
    }
}