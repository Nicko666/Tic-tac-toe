using System;
using UnityEngine;
using UnityEngine.Events;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField] private ScreenPresenter _screenPresenter;
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;
    [SerializeField] private RulesPresenter _rulesPresenter;
    private PanelsController _panelsController = new();
    private PlayerModel _selectedPlayerModel;

    public event UnityAction<PlayerModel, PlayerSettingsModel> onInputPlayerSettings;
    public event Action<SoundModel> onInputSound
    {
        add
        {
            _settingsPresenter.onInputSound += value;
            _screenPresenter.onInputSound += value;
            _playerPresenter.onInputSound += value;
            _rulesPresenter.onInputSound += value;
        }
        remove
        {
            _settingsPresenter.onInputSound -= value;
            _screenPresenter.onInputSound -= value;
            _playerPresenter.onInputSound -= value;
            _rulesPresenter.onInputSound -= value;
        }
    }
    public event Action onInputAddPlayer
    {
        add => _screenPresenter.onInputAddLastPlayer += value;
        remove => _screenPresenter.onInputAddLastPlayer -= value;
    }
    public event Action onInputRemoveLastPlayer
    {
        add => _screenPresenter.onInputRemoveLastPlayer += value;
        remove => _screenPresenter.onInputRemoveLastPlayer -= value;
    }
    public event Action<PlayerModel> onInputRemovePlayer
    {
        add => _screenPresenter.onInputRemovePlayer += value;
        remove => _screenPresenter.onInputRemovePlayer -= value;
    }
    public event Action<PlayerModel, int> onInputPlayerIndex
    {
        add => _screenPresenter.onInputPlayerIndex += value;
        remove => _screenPresenter.onInputPlayerIndex -= value;
    }
    public event Action<SceneModel> onInputGameScene
    {
        add => _screenPresenter.onInputPlayButton += value;
        remove => _screenPresenter.onInputPlayButton -= value;
    }
    public event Action<SettingsOutputModel> onInputSettings
    {
        add => _settingsPresenter.onInputSettings += value;
        remove => _settingsPresenter.onInputSettings -= value;
    }
    public event Action<RulesSettingsModel> onInputRulesSettings
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

    public void OutputFrameIntervals(FrameIntervalModel[] frameIntervals)
    {
        _settingsPresenter.OutputFrameIntervals(frameIntervals);
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
        _panelsController.onInputPanel += InputOpenPanel;
    }
    private void Start() =>
        _panelsController.SetPanel(PanelModel.None);

    private void InputOpenPanel(PanelModel panel)
    {
        _screenPresenter.OutputPanel(panel == PanelModel.None);
        _playerPresenter.OutputPanel(panel == PanelModel.Player);
        _rulesPresenter.OutputPanel(panel == PanelModel.Rules);
        _settingsPresenter.OutputPanel(panel == PanelModel.Settings);
    }

    private void InputPanel(PanelModel panel)
    {
        _selectedPlayerModel = null;
        _playerPresenter.OutputPlayer(_selectedPlayerModel);
        _panelsController.SetPanel(panel);
    }
    private void InputClosePanel()
    {
        _selectedPlayerModel = null;
        _playerPresenter.OutputPlayer(_selectedPlayerModel);
        _panelsController.SetPanel(PanelModel.None);
    }

    private void InputPlayer(PlayerModel playerModel)
    {
        _selectedPlayerModel = playerModel;
        _playerPresenter.OutputPlayer(_selectedPlayerModel);
        _panelsController.SetPanel(PanelModel.Player);
    }

    private void InputPlayerSettings(PlayerSettingsModel playerSettings) =>
        onInputPlayerSettings.Invoke(_selectedPlayerModel, playerSettings);

    public void OutputSettings(SettingsOutputModel model) =>
        _settingsPresenter.OutputSettings(model);

    class PanelsController
    {
        internal Action<PanelModel> onInputPanel;

        internal void SetPanel(PanelModel panel) =>
            onInputPanel?.Invoke(panel);
    }
}