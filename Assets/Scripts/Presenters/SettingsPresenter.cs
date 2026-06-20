using System;
using UnityEngine;
using UnityEngine.UI;
using Google.Play.AppUpdate;
using Google.Play.Common;
using System.Collections;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private TogglesTextView _framesTogglesText;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Button _updateButton;

    private AppUpdateManager _appUpdateManager;
    private FrameIntervalModel[] _frameIntervals = new FrameIntervalModel[0];
    private SettingsOutputModel _settings;
    private bool _updateAvailable;

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
        _settings = settings;
        _volumeSlider.SetValueWithoutNotify(_settings.volume);
        _framesTogglesText.OutputIsToggled(Array.FindIndex(_frameIntervals, i => i.Interval == _settings.frameInteravl.Interval));
    }

    private void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(InputVolume);
        _updateButton.onClick.AddListener(InputUpdate);
        _framesTogglesText.onInput += InputFPS;
        _panel.onInputClose += InputClosePanel;
    }
    private void OnDestroy()
    {
        _volumeSlider.onValueChanged.RemoveListener(InputVolume);
        _updateButton.onClick.RemoveListener(InputUpdate);
        _framesTogglesText.onInput -= InputFPS;
        _panel.onInputClose -= InputClosePanel;
    }

    private void Start()
    {
        _updateButton.gameObject.SetActive(_updateAvailable);
        _appUpdateManager = new();
        StartCoroutine(CheckForUpdate());
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

    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = _appUpdateManager.GetAppUpdateInfo();

        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            _updateAvailable = appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable;
            _updateButton.gameObject.SetActive(_updateAvailable);
            Debug.LogError($"_updateAvailable: {_updateAvailable}");
            
            //var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            //StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
        }
        else
            Debug.LogError("Offline");
    }

    IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    {
        var startUpdateRequest = _appUpdateManager.StartUpdate(appUpdateInfoOp_i, appUpdateOptions_i);
        yield return startUpdateRequest;
    }

    private void InputUpdate()
    {
        onInputSound.Invoke(SoundModel.Accept);
        //Application.OpenURL("market://details?id=" + Application.identifier);
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
}