using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class GamePlayersPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private Button _menuSceneButton;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GamePlayerPresenter _gamePlayerPrefab;
    private readonly List<GamePlayerPresenter> _gamePlayers = new();

    internal Action<PanelModel> onInputGamePlayers;
    internal Action<SceneModel> onInputScene;
    internal Action<SoundModel> onInputSound;

    internal void OutputPanel(bool value) =>
        _panel.OutputOpen(value);

    internal void OutputGamePlayers(GamePlayersModel gamePlayers)
    {
        while (_gamePlayers.Count > gamePlayers.gamePlayers.Length)
            _gamePlayers.RemoveAt(_gamePlayers.Count - 1);
        while (_gamePlayers.Count < gamePlayers.gamePlayers.Length)
            _gamePlayers.Add(Instantiate(_gamePlayerPrefab, _scrollRect.content));
        for (int i = 0; i < gamePlayers.gamePlayers.Length; i++)
        {
            _gamePlayers[i].Output(gamePlayers.gamePlayers[i]);
        }
    }

    internal void OutputWin() =>
        _panel.OutputInteractable(false);

    private void Awake()
    {
        _menuSceneButton.onClick.AddListener(InputMenuScene);
        _panel.onInputClose += InputBoard;
    }
    private void OnDestroy()
    {
        _menuSceneButton.onClick.RemoveListener(InputMenuScene);
        _panel.onInputClose -= InputBoard;
    }

    private void InputBoard()
    {
        onInputGamePlayers.Invoke(PanelModel.Board);
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputMenuScene()
    {
        onInputScene.Invoke(SceneModel.MenuScene);
        onInputSound.Invoke(SoundModel.Accept);
    }
}