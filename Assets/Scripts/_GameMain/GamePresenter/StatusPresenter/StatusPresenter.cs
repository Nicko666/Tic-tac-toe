using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel, _queuePanel, _winnerPanel;
    [SerializeField] private Button _settingsButton, _gamePlayersButton;
    [SerializeField] private PlayersQueuePresenter _playersQueue;
    [SerializeField] private PlayersWinnerPresenter _playersWinner;
    private readonly List<IEnumerator> _routines = new();
    private Coroutine _coroutine = null;

    internal Action<PanelModel> onInputPanel;
    internal Action<SoundModel> onInputSound;
    internal Action onInputClearBoard;

    internal void OutputPanel(bool isOpen) =>
        _panel.OutputOpen(isOpen);

    internal void OutputPlayer(GamePlayerModel gamePlayerModel, float duration)
    {
        _routines.Add(OutputPlayerRoutine(gamePlayerModel.player, duration));
        _coroutine ??= StartCoroutine(OutputQueueCoroutine());

        IEnumerator OutputPlayerRoutine(PlayerModel player, float duration)
        {
            _playersQueue.OutputPlayer(player);
            yield return new WaitForSeconds(duration);
        }
    }

    internal void OutputBoard(GameBoardModel gameBoard)
    {
        _routines.Add(OutputClosePanelsRoutine(gameBoard, 0f));
        _coroutine ??= StartCoroutine(OutputQueueCoroutine());

        IEnumerator OutputClosePanelsRoutine(GameBoardModel gameBoard, float duration)
        {
            _queuePanel.OutputOpen(gameBoard.IsInteractable);
            _winnerPanel.OutputOpen(!gameBoard.IsInteractable);
            if (!gameBoard.IsInteractable)
                _playersWinner.OutputPlayer(gameBoard.Winner);

            yield return new WaitForSeconds(duration);
        }
    }

    private IEnumerator OutputQueueCoroutine()
    {
        while (_routines.Count > 0)
        {
            yield return _routines[0];
            _routines.RemoveAt(0);
        }
        _coroutine = null;
    }

    private void Awake()
    {
        _settingsButton.onClick.AddListener(InputSettings);
        _gamePlayersButton.onClick.AddListener(InputGamePlayers);

    }
    private void OnDestroy()
    {
        _settingsButton.onClick.RemoveListener(InputSettings);
        _gamePlayersButton.onClick.RemoveListener(InputGamePlayers);
    }

    private void InputSettings()
    {
        onInputPanel.Invoke(PanelModel.Settings);
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputGamePlayers()
    {
        onInputPanel.Invoke(PanelModel.Players);
        onInputSound.Invoke(SoundModel.Accept);
    }
}