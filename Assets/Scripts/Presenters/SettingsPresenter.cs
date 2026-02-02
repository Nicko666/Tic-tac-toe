using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private TogglesTextView _framesTogglesText;
    [SerializeField] private Slider _volumeSlider;
    
    private FrameIntervalModel[] _frameIntervals = new FrameIntervalModel[0];
    private SettingsOutputModel _settings;

    public Action onInputClosePanel;
    public Action<SettingsOutputModel> onInputSettings;
    public Action<SoundModel> onInputSound;

    public void OutputFrameIntervals(FrameIntervalModel[] frameIntervals)
    {
        _frameIntervals = frameIntervals;
        _framesTogglesText.OutputToggles(Array.ConvertAll(_frameIntervals, i => $"{i.FramesCount}p"));
        if (Array.Exists(_frameIntervals, i => i.Interval == _settings.frameInteravl.Interval))
            _framesTogglesText.OutputIsToggled(Array.FindIndex(_frameIntervals, i => i.Interval == _settings.frameInteravl.Interval));

        _framesTogglesText.gameObject.SetActive(_frameIntervals.Length > 1);
    }

    public void OutputPanel(bool value) =>
        _panel.OutputOpen(value);

    public void OutputSettings(SettingsOutputModel settings)
    {
        Debug.Log($"OutputSettings{settings.frameInteravl.Interval}/{settings.frameInteravl.FramesCount}");
        _settings = settings;
        Debug.Log($"OutputSettings{_settings.frameInteravl.Interval}/{_settings.frameInteravl.FramesCount}");
        _volumeSlider.SetValueWithoutNotify(_settings.volume);
        _framesTogglesText.OutputIsToggled(Array.FindIndex(_frameIntervals, i => i.Interval == _settings.frameInteravl.Interval));
    }

    private void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(InputVolume);
        _framesTogglesText.onInput += InputFPS;
        _panel.onInputClose += InputClosePanel;
    }

    private void InputClosePanel()
    {
        onInputClosePanel.Invoke();
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputVolume(float value)
    {
        _settings.volume = value;
        onInputSettings.Invoke(_settings);
    }

    private void InputFPS(int index)
    {
        _settings.frameInteravl = _frameIntervals[index];
        onInputSettings.Invoke(_settings);
        onInputSound.Invoke(SoundModel.Accept);
    }
}