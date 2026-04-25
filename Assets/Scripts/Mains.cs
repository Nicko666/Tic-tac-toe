using System;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class Mains : MonoBehaviour
{
    [SerializeField] private TextAsset _progressEncryptionCodeFile;
    [SerializeField] private ProgressDatabase _progressDatabase;
    [SerializeField] private LoadingPresenter _loadingPresenterPrefab;
    [SerializeField] private SoundPresenter _soundPresenterPrefab;
    [SerializeField] private IMain _main;

    private const string ProgressFileName = "Progress", SettingsFileName = "Settings";

    private SettingsDatabaseController _settingsDatabaseController = new();
    private SaveController<SettingsData> _settingsDataController;
    private SettingsController _settingsController = new();
    private SaveController<ProgressData> _progressDataController;
    private ProgressController _progressController = new();

    private static LoadingPresenter LoadingPresenter;
    private static SoundPresenter SoundPresenter;

    private ProgressData _progressData;
    private ProgressModel _progress = new();
    private SettingsData _settingsData;

    private void Awake()
    {
        DontDestroyOnLoad(LoadingPresenter ??= Instantiate(_loadingPresenterPrefab));
        DontDestroyOnLoad(SoundPresenter ??= Instantiate(_soundPresenterPrefab));

        _progressDataController = new(Application.persistentDataPath, ProgressFileName, _progressEncryptionCodeFile? _progressEncryptionCodeFile.text : "");
        if (!_progressEncryptionCodeFile) Debug.Log("Add EncryptionCodeFile before build");
        _settingsDataController = new(Application.persistentDataPath, SettingsFileName);

        SceneManager.sceneLoaded += Load;
        Display.onDisplaysUpdated += _settingsDatabaseController.Update;
        _settingsDatabaseController.onChanged += OutputSettingsDatabase;
        _settingsController.onSettingsChanged += OutputSettings;
        _settingsController.onInputSaveSettings += SaveSettings;

        _main.onInputSound += SoundPresenter.OutputSound;
        _main.onInputScene += InputGameScene;
        _main.onInputSettings += _settingsController.SetSettings;
        _main.onInputSaveProgress += SaveProgress;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= Load;
        Display.onDisplaysUpdated -= _settingsDatabaseController.Update;
        _settingsDatabaseController.onChanged -= OutputSettingsDatabase;
        _settingsController.onSettingsChanged -= OutputSettings;
        _settingsController.onInputSaveSettings -= SaveSettings;

        _main.onInputSound -= SoundPresenter.OutputSound;
        _main.onInputScene -= InputGameScene;
        _main.onInputSettings -= _settingsController.SetSettings;
        _main.onInputSaveProgress -= SaveProgress;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            _settingsDatabaseController.Update();
            _settingsData = _settingsDataController.Load();
            _settingsController.SetData(_settingsData);
        }
    }
    
    private void OutputSettingsDatabase(FrameIntervalModel[] frameIntervals)
    {
        _main.OutputSettingsDatabase(frameIntervals);
        _settingsController.OutputSettingsDatabase(frameIntervals);
    }

    private void OutputSettings(SettingsOutputModel settings)
    {
        SoundPresenter.OutputSettings(settings.volume);
        _main.OutputSettings(settings);
    }

    private void InputGameScene(SceneModel sceneModel) =>
        LoadingPresenter.OutputGameScene(sceneModel);

    private void Load(Scene scene, LoadSceneMode loadSceneMode)
    {
        _settingsDatabaseController.Update();
        _settingsData = _settingsDataController.Load();
        _settingsController.SetData(_settingsData);

        _progressController.SetDatabase(_progressDatabase);
        _main.OutputProgressDatabase(_progressDatabase);
        _progressData = _progressDataController.Load();
        _progressController.GetProgress(ref _progress, _progressData);
        _main.LoadProgress(_progress);
    }

    private void SaveSettings()
    {
        _settingsController.GetData(ref _settingsData);
        _settingsDataController.Save(_settingsData);
    }
    private void SaveProgress()
    {
        _main.SaveProgress(ref _progress);
        _progressController.GetData(ref _progressData, _progress);
        _progressDataController.Save(_progressData);
    }
}