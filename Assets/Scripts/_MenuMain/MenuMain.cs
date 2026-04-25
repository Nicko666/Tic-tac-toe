using System;
using UnityEngine;

[Serializable]
internal class MenuMain : IMain
{
    private MenuPlayersController _playersController = new();
    private MenuRulesController _rulesController = new();
    [SerializeField] private MenuPresenter _menuPresenter;

    internal override event Action<SoundModel> onInputSound
    {
        add => _menuPresenter.onInputSound += value;
        remove => _menuPresenter.onInputSound -= value;
    }
    internal override event Action<SceneModel> onInputScene
    {
        add => _menuPresenter.onInputGameScene += value;
        remove => _menuPresenter.onInputGameScene -= value;
    }
    internal override event Action<SettingsOutputModel> onInputSettings
    {
        add => _menuPresenter.onInputSettings += value;
        remove => _menuPresenter.onInputSettings -= value;
    }
    internal override event Action onInputSaveProgress
    {
        add
        {
            _playersController.onInputSaveProgress += value;
            _rulesController.onInputSaveProgress += value;
        }
        remove
        {
            _playersController.onInputSaveProgress -= value;
            _rulesController.onInputSaveProgress -= value;
        }
    }

    internal override void OutputProgressDatabase(ProgressDatabase database)
    {
        _playersController.SetDatabase(database.MinPlayersCount, database.Logics, database.Marks);
        _menuPresenter.OutputProgressDatabase(database.Marks, database.Logics, database.Boards, database.Levels);
    }

    internal override void OutputSettingsDatabase(FrameIntervalModel[] frameIntervals) =>
        _menuPresenter.OutputFrameIntervals(frameIntervals);
   
    internal override void OutputSettings(SettingsOutputModel settings) =>
        _menuPresenter.OutputSettings(settings);

    internal override void LoadProgress(ProgressModel progress)
    {
        _playersController.LoadPlayers(progress.players);
        _rulesController.LoadRules(progress.rules);
    }

    internal override void SaveProgress(ref ProgressModel progress)
    {
        _playersController.SavePlayers(ref progress.players);
        _rulesController.SaveRules(ref progress.rules);
    }

    private void Awake()
    {
        _menuPresenter.onInputAddPlayer += _playersController.AddPlayer;
        _menuPresenter.onInputRemoveLastPlayer += _playersController.RemoveLastPlayer;
        _menuPresenter.onInputRemovePlayer += _playersController.RemovePlayer;
        _menuPresenter.onInputPlayerIndex += _playersController.SetPlayerIndex;
        _menuPresenter.onInputPlayerSettings += _playersController.SetPlayerSettings;
        _menuPresenter.onInputRulesSettings += _rulesController.SetRulesSettings;
        _playersController.onChanged += _menuPresenter.OutputPlayers;
        _rulesController.onChanged += _menuPresenter.OutputRules;
    }
}