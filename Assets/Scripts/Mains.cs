using System;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class Mains : MonoBehaviour
{
    private const string ProgressFileName = "Progress", ProgressEncryptionCode = "", SettingsFileName = "Settings";

    private SettingsController _settingsController = new();
    private PlayersController _playersController = new();
    private RulesController _rulesController = new();
    private DisplayDatabaseController _displayDatabaseController = new();
    private SaveController<SettingsData> _settingsDataController;
    private SaveController<ProgressData> _progressDataController;

    [SerializeField] private IMain[] _mains;
    [SerializeField] private ProgressDatabase _progressDatabase;
    [SerializeField] private LoadingPresenter _loadingPresenterPrefab;
    [SerializeField] private SoundPresenter _soundPresenterPrefab;

    private static LoadingPresenter LoadingPresenter;
    private static SoundPresenter SoundPresenter;

    private void Awake()
    {
        DontDestroyOnLoad(LoadingPresenter ??= Instantiate(_loadingPresenterPrefab));
        DontDestroyOnLoad(SoundPresenter ??= Instantiate(_soundPresenterPrefab));

        _progressDataController = new(Application.persistentDataPath, ProgressFileName, ProgressEncryptionCode);
        _progressDataController.onDataLoaded += OutputProgressDataLoaded;
        _progressDataController.onDataSaved += OutputProgressDataSaved;

        _settingsDataController = new(Application.persistentDataPath, SettingsFileName);
        _settingsDataController.onDataLoaded += OutputSettingsDataLoaded;
        _settingsDataController.onDataSaved += OutputSettingsDataSaved;

        Display.onDisplaysUpdated += OutputDisplayUpdated;
        SceneManager.sceneLoaded += OutputSceneLoaded;
        _displayDatabaseController.onChanged += OutputDisplayDatabase;
        _settingsController.onSettingsChanged += OutputSettings;
        _settingsController.onInputSaveSettings += _settingsDataController.Save;
        _rulesController.onRulesChanged += OutputRules;
        _playersController.onPlayersChanged += OutputPlayers;

        _displayDatabaseController.Set();
        _rulesController.SetDatabase(_progressDatabase.Levels, _progressDatabase.Boards);
        _playersController.SetDatabase(_progressDatabase.MinPlayersCount, _progressDatabase.Logics, _progressDatabase.Marks);

        Array.ForEach(_mains, main =>
        {
            main.onInputSound += SoundPresenter.OutputSound;
            main.onInputScene += InputGameScene;
            main.onInputSettings += _settingsController.SetSettings;
            main.onInputSaveProgress += _progressDataController.Save;

            main.OutputProgressDatabase(_progressDatabase);
        });
    }

    private void OnDestroy()
    {
        _progressDataController.onDataLoaded -= OutputProgressDataLoaded;
        _progressDataController.onDataSaved -= OutputProgressDataSaved;

        _settingsDataController.onDataLoaded -= OutputSettingsDataLoaded;
        _settingsDataController.onDataSaved -= OutputSettingsDataSaved;

        Display.onDisplaysUpdated -= OutputDisplayUpdated;
        SceneManager.sceneLoaded -= OutputSceneLoaded;
        _displayDatabaseController.onChanged -= OutputDisplayDatabase;
        _settingsController.onSettingsChanged -= OutputSettings;
        _settingsController.onInputSaveSettings -= _settingsDataController.Save;
        _rulesController.onRulesChanged -= OutputRules;
        _playersController.onPlayersChanged -= OutputPlayers;

        Array.ForEach(_mains, main =>
        {
            main.onInputSound -= SoundPresenter.OutputSound;
            main.onInputScene -= InputGameScene;
            main.onInputSettings -= _settingsController.SetSettings;
            main.onInputSaveProgress -= _progressDataController.Save;
        });
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            _displayDatabaseController.Set();
    }

    private void OutputDisplayUpdated() =>
        _displayDatabaseController.Set();

    private void OutputDisplayDatabase(FrameIntervalModel[] frameIntervals)
    {
        Array.ForEach(_mains, main => main.OutputFrameIntervals(frameIntervals));
        _settingsController.SetDisplayDatabase(frameIntervals);
    }

    private void OutputPlayers(PlayerModel[] players) =>
        Array.ForEach(_mains, main => main.OutputPlayers(players));
    private void OutputRules(RulesModel rules) =>
        Array.ForEach(_mains, main => main.OutputRules(rules));
    private void OutputSettings(SettingsOutputModel settings)
    {
        SoundPresenter.OutputSettings(settings.volume);
        Array.ForEach(_mains, main => main.OutputSettings(settings));
    }

    private void InputGameScene(SceneModel sceneModel) =>
        LoadingPresenter.OutputGameScene(sceneModel);

    private void OutputSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _settingsDataController.Load();
        _progressDataController.Load();
    }

    private void OutputSettingsDataLoaded(SettingsData data)
    {
        _settingsController.LoadSettingsData(data);
    }
    private void OutputSettingsDataSaved(ref SettingsData data)
    {
        _settingsController.SaveSettingsData(ref data);
    }

    private void OutputProgressDataLoaded(ProgressData data)
    {
        _rulesController.SetRulesData(data.levelsID, data.boardID);
        _playersController.SetPlayersData(data.playersData);

        foreach (var i in _mains) i.OutputLoadedProgressData(data);
    }
    private void OutputProgressDataSaved(ref ProgressData data)
    {
        foreach (var i in _mains) i.OutputSavedProgressData(ref data);
    }
}