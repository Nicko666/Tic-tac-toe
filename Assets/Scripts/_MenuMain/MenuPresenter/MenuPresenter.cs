using System;
using UnityEngine;
using UnityEngine.Events;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField] private LoadinfPresenter _screenPresenter;
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;
    [SerializeField] private RulesPresenter _rulesPresenter;
    private PlayerModel _selectedPlayerModel;

    public event UnityAction<PlayerModel, PlayerSettingsModel> onInputPlayerSettings;
    public event UnityAction onInputAddPlayer
    {
        add => _screenPresenter.onInputAddLastPlayer += value;
        remove => _screenPresenter.onInputAddLastPlayer -= value;
    }
    public event UnityAction onInputRemoveLastPlayer
    {
        add => _screenPresenter.onInputRemoveLastPlayer += value;
        remove => _screenPresenter.onInputRemoveLastPlayer -= value;
    }
    public event UnityAction<PlayerModel> onInputRemovePlayer
    {
        add => _screenPresenter.onInputRemovePlayer += value;
        remove => _screenPresenter.onInputRemovePlayer -= value;
    }
    public event UnityAction<PlayerModel, int> onInputPlayerIndex
    {
        add => _screenPresenter.onInputPlayerIndex += value;
        remove => _screenPresenter.onInputPlayerIndex -= value;
    }
    public event Action<SceneModel> onInputGameScene
    {
        add => _screenPresenter.onInputPlayButton += value;
        remove => _screenPresenter.onInputPlayButton -= value;
    }
    public event Action<SettingsModel> onInputSettings
    {
        add => _settingsPresenter.onInputSettings += value;
        remove => _settingsPresenter.onInputSettings -= value;
    }
    public event UnityAction<RulesSettingsModel> onInputRulesSettings
    {
        add => _rulesPresenter.onInputRulesSettings += value;
        remove => _rulesPresenter.onInputRulesSettings -= value;
    }

    public void OutputRules(RulesModel rules)
    {
        _screenPresenter.OutputRules(rules);
        _rulesPresenter.OutputRules(rules);
    }

    public void OutputProgressDatabase(MarkModel[] marks, LogicModel[] logics, BoardModel[] boards, LevelModel[] levelsModels)
    {
        _playerPresenter.OutputDatabase(marks, logics);
        _rulesPresenter.OutputDatabase(boards, levelsModels);
    }

    public void OutputSettingsDatabase(SettingsDatabase settingsDatabase)
    {
        _settingsPresenter.OutputDatabase(settingsDatabase);
    }

    public void OutputPlayers(PlayerModel[] players)
    {
        _screenPresenter.OutputPlayers(players);
        _playerPresenter.OutputPlayer(_selectedPlayerModel);
    }

    private void Awake()
    {
        _screenPresenter.onInputPanel += InputPanel;
        _screenPresenter.onInputPlayer += InputPlayer;
        _playerPresenter.onInputClosePanel += InputClosePanel;
        _playerPresenter.onInputPlayerSettings += InputPlayerSettings;
        _settingsPresenter.onInputClosePanel += InputClosePanel;
        _rulesPresenter.onInputClosePanel += InputClosePanel;
    }
    private void Start()
    {
        InputPanel(PanelModel.None);
    }

    private void InputPanel(PanelModel panelModel)
    {
        _selectedPlayerModel = null;
        _playerPresenter.OutputPlayer(_selectedPlayerModel);
        
        _screenPresenter.OutputPanel(panelModel == PanelModel.None);
        _playerPresenter.OutputPanel(panelModel == PanelModel.Player);
        _rulesPresenter.OutputPanel(panelModel == PanelModel.Rules);
        _settingsPresenter.OutputPanel(panelModel == PanelModel.Settings);
    }
    private void InputClosePanel()
    {
        _selectedPlayerModel = null;
        _playerPresenter.OutputPlayer(_selectedPlayerModel);
        
        _screenPresenter.OutputPanel(true);
        _playerPresenter.OutputPanel(false);
        _rulesPresenter.OutputPanel(false);
        _settingsPresenter.OutputPanel(false);
    }

    private void InputPlayer(PlayerModel playerModel)
    {
        _selectedPlayerModel = playerModel;
        _playerPresenter.OutputPlayer(_selectedPlayerModel);
        
        _screenPresenter.OutputPanel(false);
        _playerPresenter.OutputPanel(true);
        _screenPresenter.OutputPanel(false);
        _settingsPresenter.OutputPanel(false);
    }

    private void InputPlayerSettings(PlayerSettingsModel playerSettings) =>
        onInputPlayerSettings.Invoke(_selectedPlayerModel, playerSettings);

    public void OutputSettings(SettingsModel model) =>
        _settingsPresenter.OutputSettings(model);
}