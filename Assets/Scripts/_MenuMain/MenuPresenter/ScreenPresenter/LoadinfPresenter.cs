using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class LoadinfPresenter : MonoBehaviour
{
    [SerializeField] private PanelsButton[] _panelsButtons;
    [SerializeField] private RulesButtonPresenter _rulesButton;
    [SerializeField] private PlayersListPresenter _playersList;
    [SerializeField] private Button _playButton;
    [SerializeField] private PanelView _panel;

    internal Action<PanelModel> onInputPanel;
    internal Action<SceneModel> onInputPlayButton;

    internal event UnityAction<PlayerModel> onInputPlayer
    {
        add => _playersList.onInputPlayer += value;
        remove => _playersList.onInputPlayer -= value;
    }
    public event UnityAction onInputAddLastPlayer
    {
        add => _playersList.onInputAddLastPlayer += value;
        remove => _playersList.onInputAddLastPlayer -= value;
    }
    public event UnityAction onInputRemoveLastPlayer
    {
        add => _playersList.onInputRemoveLastPlayer += value;
        remove => _playersList.onInputRemoveLastPlayer -= value;
    }
    internal event UnityAction<PlayerModel> onInputRemovePlayer
    {
        add => _playersList.onInputRemovePlayer += value;
        remove => _playersList.onInputRemovePlayer -= value;
    }    
    public event UnityAction<PlayerModel, int> onInputPlayerIndex
    {
        add => _playersList.onInputPlayerIndex += value;
        remove => _playersList.onInputPlayerIndex -= value;
    }


    internal void OutputPanel(bool value) =>
        _panel.OutputOpen(value);

    internal void OutputRules(RulesModel rules) =>
        _rulesButton.OutputFieldModel(rules);

    internal void OutputPlayers(PlayerModel[] players) =>
        _playersList.OutputPlayers(players);

    private void Awake()
    {
        Array.ForEach(_panelsButtons, i => i.Button.onClick.AddListener(InputPanelsButton));
        _rulesButton.onInputButton += InputRulesButton;
        _playButton.onClick.AddListener(InputSceneModel);
    }

    private void InputSceneModel() =>
        onInputPlayButton.Invoke(SceneModel.GameScene);

    private void InputPanelsButton()
    {
        GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
        PanelsButton panelsButton = Array.Find(_panelsButtons, i => i.Button.gameObject == buttonObject);
        
        onInputPanel.Invoke(panelsButton.Panel);
    }

    private void InputRulesButton() =>
        onInputPanel.Invoke(PanelModel.Rules);

    [Serializable]
    class PanelsButton
    {
        [field: SerializeField] internal PanelModel Panel { get; private set; }
        [field: SerializeField] internal Button Button { get; private set; }
    }
}
