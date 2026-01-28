using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private Button _settingsButton, _gamePlayersButton;
    [SerializeField] private PlayersQueuePresenter _playersQueue;
    private readonly List<IEnumerator> _routines = new();
    private Coroutine _coroutine = null;

    internal Action<PanelModel> onInputPanel;

    internal void OutputPanel(bool isOpen) =>
        _panel.OutputOpen(isOpen);
    
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

    private void InputSettings() =>
        onInputPanel.Invoke(PanelModel.Settings);

    private void InputGamePlayers() =>
        onInputPanel.Invoke(PanelModel.Players);

    internal void OutputPlayer(GamePlayerModel gamePlayerModel, float duration)
    {
        _routines.Add(OutputPlayerRoutine(gamePlayerModel.player, duration));

        _coroutine ??= StartCoroutine(OutputQueueCoroutine());
        IEnumerator OutputQueueCoroutine()
        {
            while (_routines.Count > 0)
            {
                yield return _routines[0];
                _routines.RemoveAt(0);
            }
            _coroutine = null;
        }
    }

    IEnumerator OutputPlayerRoutine(PlayerModel player, float duration)
    {
        _playersQueue.OutputPlayer(player);
        yield return new WaitForSeconds(duration);
    }
}
