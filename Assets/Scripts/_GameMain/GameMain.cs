using System;
using UnityEngine;

internal class GameMain : IMain
{
    private GameController _gameController = new();
    [SerializeField] private GamePresenter _gamePresenter;

    internal override event Action onInputSaveProgress;
    internal override event Action<SettingsModel> onInputSettings
    {
        add => _gamePresenter.onInputSettings += value;
        remove => _gamePresenter.onInputSettings -= value;
    }
    internal override event Action<SceneModel> onInputScene
    {
        add => _gamePresenter.onInputScene += value;
        remove => _gamePresenter.onInputScene -= value;
    }

    internal override void OutputPlayers(PlayerModel[] players) =>
       _gameController.LoadPlayers(players);

    internal override void OutputRules(RulesModel rules) =>
        _gameController.LoadRules(rules);

    internal override void OutputSettings(SettingsModel settings) =>
        _gamePresenter.OutputSettings(settings);

    internal override void OutputProgressDatabase(ProgressDatabase database) { }

    internal override void LoadProgressData(ProgressData data) { }

    internal override void OutputSettingsDatabase(SettingsDatabase SettingsDatabase) =>
        _gamePresenter.OutputSettingsDatabase(SettingsDatabase);

    internal override void SaveProgressData(ref ProgressData data) { }

    private void Awake()
    {
        _gameController.onBoardChanged += _gamePresenter.OutputBoard;
        _gameController.onPlayersChanged += _gamePresenter.OutputPlayers;
        _gameController.onPlayersQueueChanged += _gamePresenter.OutputPlayersQueue;
        _gamePresenter.onInputTile += _gameController.InputTile;
        _gamePresenter.onInputClearBoard += _gameController.InputRestart;
    }
    private void OnDestroy()
    {
        _gameController.onBoardChanged -= _gamePresenter.OutputBoard;
        _gameController.onPlayersChanged -= _gamePresenter.OutputPlayers;
        _gameController.onPlayersQueueChanged -= _gamePresenter.OutputPlayersQueue;
        _gamePresenter.onInputTile -= _gameController.InputTile;
        _gamePresenter.onInputClearBoard -= _gameController.InputRestart;
    }

    private void InputSaveProgress() =>
        onInputSaveProgress.Invoke();
}