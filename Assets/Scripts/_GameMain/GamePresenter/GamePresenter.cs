using System;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    PanelsController _panelsController = new();
    [SerializeField] private BoardPresenter _board;
    [SerializeField] private StatusPresenter _status;
    [SerializeField] private GamePlayersPresenter _gamePlayers;
    [SerializeField] private SettingsPresenter _settings;
    [SerializeField] private float _computerDuration;
    
    public event Action<Vector2Int> onInputTile
    {
        add => _board.onInputTile += value;
        remove => _board.onInputTile -= value;
    }
    public event Action<SettingsModel> onInputSettings
    {
        add => _settings.onInputSettings += value;
        remove => _settings.onInputSettings -= value;
    }
    public event Action onInputClearBoard
    {
        add => _board.onInputClearBoard += value;
        remove => _board.onInputClearBoard -= value;
    }
    public event Action<SceneModel> onInputScene
    {
        add => _gamePlayers.onInputScene += value;
        remove => _gamePlayers.onInputScene -= value;
    }

    public void OutputSettingsDatabase(SettingsDatabase settingsDatabase) =>
        _settings.OutputDatabase(settingsDatabase);
    public void OutputSettings(SettingsModel settings) =>
        _settings.OutputSettings(settings);

    public void OutputBoard(GameBoardModel gameBoard) =>
        _board.OutputBoard(gameBoard);

    public void OutputPlayers(GamePlayersModel gamePlayers)
    {
        _gamePlayers.OutputGamePlayers(gamePlayers);
        if (gamePlayers.winner != null)
            _panelsController.InputPanel(PanelModel.Players);
    }
    
    private void Awake()
    {
        _status.onInputPanel += _panelsController.InputPanel;
        _gamePlayers.onInputGamePlayers += _panelsController.InputPanel;
        _settings.onInputClosePanel += InputDefaultPanel;
        _panelsController.onInputPlayers += _gamePlayers.OutputPanel;
        _panelsController.onInputSettings += _settings.OutputPanel;
        _panelsController.onInputStatus += _status.OutputPanel;
    }
    private void OnDestroy()
    {
        _status.onInputPanel -= _panelsController.InputPanel;
        _gamePlayers.onInputGamePlayers -= _panelsController.InputPanel;
        _settings.onInputClosePanel -= InputDefaultPanel;
        _panelsController.onInputPlayers -= _gamePlayers.OutputPanel;
        _panelsController.onInputSettings -= _settings.OutputPanel;
        _panelsController.onInputStatus -= _status.OutputPanel;
    }

    private void InputDefaultPanel()
    {
        _status.OutputPanel(true);
        _settings.OutputPanel(false);
    }

    public void OutputPlayersQueue(GamePlayerModel[] gamePlayers)
    {
        bool isComputer = gamePlayers[0].player.logic.Logic != LogicModel.LogicType.Player;
        _board.OutputDelay(isComputer ? _computerDuration : 0); 
        _status.OutputPlayer(gamePlayers[0], isComputer ? _computerDuration : 0);
    }

    class PanelsController
    {
        internal Action<bool> onInputStatus, onInputSettings, onInputPlayers;

        internal void InputPanel(PanelModel gamePanel)
        {
            onInputStatus.Invoke(gamePanel == PanelModel.Board);
            onInputPlayers.Invoke(gamePanel == PanelModel.Players);
            onInputSettings.Invoke(gamePanel == PanelModel.Settings);
        }
    }
}