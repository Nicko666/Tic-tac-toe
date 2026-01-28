using System;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class Mains : MonoBehaviour
{
    private SettingsDatabaseController _appDatabaseController = new();
    private SettingsController _settingsController = new();
    private MainController _mainController = new();
    private DataController _dataController = new();

    [SerializeField] private IMain[] _mains;
    [SerializeField] private ProgressDatabase _progressDatabase;
    [SerializeField] private LoadingPresenter _presenterPrefab;
    
    private SettingsDatabase _settingsDatabase;
    private ProgressData _progress;
    private SettingsData _settingsData;

    private static LoadingPresenter Presenter;

    private void Awake()
    {
        DontDestroyOnLoad(Presenter ??= Instantiate(_presenterPrefab));
        _mainController.SetDatabase(_progressDatabase.MinPlayersCount, _progressDatabase.LogicsModel, _progressDatabase.Marks);
        
        _settingsDatabase = _appDatabaseController.Get();


        _settingsController.SetDatabase(_settingsDatabase);

        SceneManager.sceneLoaded += InputLoadProgress;
        SceneManager.sceneLoaded += InputLoadSettings;
        _settingsController.onInputSaveSettings += InputSaveSettings;
        _settingsController.onSettingsChanged += OutputSettings;
        _mainController.onRulesChanged += OutputRules;
        _mainController.onPlayersChanged += OutputPlayers;
        Array.ForEach(_mains, main =>
        {
            main.onInputScene += InputGameScene;
            main.onInputSettings += _settingsController.SetSettings;
            main.onInputSaveProgress += InputSaveProgress;

            main.OutputProgressDatabase(_progressDatabase);
            main.OutputSettingsDatabase(_settingsDatabase);
        });
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= InputLoadProgress;
        SceneManager.sceneLoaded -= InputLoadSettings;
        _settingsController.onInputSaveSettings -= InputSaveSettings;
        _settingsController.onSettingsChanged -= OutputSettings;
        _mainController.onRulesChanged -= OutputRules;
        _mainController.onPlayersChanged -= OutputPlayers;
        Array.ForEach(_mains, main =>
        {
            main.onInputScene -= InputGameScene;
            main.onInputSettings -= _settingsController.SetSettings;
            main.onInputSaveProgress -= InputSaveProgress;
        });
    }

    private void OutputPlayers(PlayerModel[] players) =>
        Array.ForEach(_mains, main => main.OutputPlayers(players));
    private void OutputRules(RulesModel rules) =>
        Array.ForEach(_mains, main => main.OutputRules(rules));
    private void OutputSettings(SettingsModel model) =>
        Array.ForEach(_mains, main => main.OutputSettings(model));

    private void InputGameScene(SceneModel sceneModel) =>
        Presenter.OutputGameScene(sceneModel);

    private void InputLoadProgress(Scene scene, LoadSceneMode loadSceneMode)
    {
        _progress = _dataController.LoadProgressData();

        LoadProgressData(_progress);
        
        foreach (var i in _mains) i.LoadProgressData(_progress);

        void LoadProgressData(ProgressData data)
        {
            RulesModel rules;
            rules.levels = Array.Find(_progressDatabase.LevelsModel, i => i.ID == data.levelsID);
            rules.board = Array.Find(_progressDatabase.BoardsModel, i => i.ID == data.boardID);
            _mainController.SetRules(rules);

            PlayerModel[] players = Array.ConvertAll(data.playersData, playerData =>
            {
                PlayerModel player = new PlayerModel();
                player.logic = Array.Find(_progressDatabase.LogicsModel, i => i.ID == playerData.logicID);
                player.mark = Array.Find(_progressDatabase.Marks, i => i.ID == playerData.markID);
                player.hue = playerData.hue;
                player.saturation = playerData.saturation;
                return player;
            });
            _mainController.SetPlayers(players);
        }
    }
    private void InputSaveProgress()
    {
        if (_progress == null) return;

        foreach (var i in _mains) i.SaveProgressData(ref _progress);
        _dataController.SaveProgressData(_progress);
    }

    private void InputLoadSettings(Scene scene, LoadSceneMode loadSceneMode)
    {
        _settingsData = _dataController.LoadSettingsData();
        SettingsModel settings = new SettingsModel();
        settings.frameInteravl = 
            Array.Exists(_settingsDatabase.FrameIntervals, i => i.Interval == _settingsData.frameInterval) ?
            Array.Find(_settingsDatabase.FrameIntervals, i => i.Interval == _settingsData.frameInterval) : _settingsDatabase.FrameIntervals[0];
        settings.volume = _settingsData.volume;

        _settingsController.SetSettings(settings);
    }
    private void InputSaveSettings()
    {
        if (_settingsData == null) return;

        SettingsModel settings = new SettingsModel();
        _settingsController.GetSettings(ref settings);
        _settingsData.volume = settings.volume;
        _dataController.SaveSettingsData(_settingsData);
    }
}