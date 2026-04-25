using System;
using UnityEngine;

internal class GameMain : IMain
{
    private GameController _gameController = new();
    [SerializeField] private GamePresenter _gamePresenter;

    internal override event Action onInputSaveProgress;
    internal override event Action<SoundModel> onInputSound
    {
        add => _gamePresenter.onInputSound += value;
        remove => _gamePresenter.onInputSound -= value;
    }
    internal override event Action<SettingsOutputModel> onInputSettings
    {
        add => _gamePresenter.onInputSettings += value;
        remove => _gamePresenter.onInputSettings -= value;
    }
    internal override event Action<SceneModel> onInputScene
    {
        add => _gamePresenter.onInputScene += value;
        remove => _gamePresenter.onInputScene -= value;
    }

    internal override void OutputSettingsDatabase(FrameIntervalModel[] frameIntervals) =>
        _gamePresenter.OutputFrameIntervals(frameIntervals);
    internal override void OutputProgressDatabase(ProgressDatabase database) { }

    internal override void LoadProgress(ProgressModel progress)
    {
       _gameController.LoadPlayers(progress.players);
       _gameController.LoadRules(progress.rules);    
    }
    internal override void SaveProgress(ref ProgressModel data) { }

    internal override void OutputSettings(SettingsOutputModel settings) =>
        _gamePresenter.OutputSettings(settings);

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