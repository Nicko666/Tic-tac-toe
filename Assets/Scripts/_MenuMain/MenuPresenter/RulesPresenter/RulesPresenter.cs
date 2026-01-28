using System;
using UnityEngine;
using UnityEngine.Events;

internal class RulesPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private TogglesImageView _boardsToggles, _levelsToggles;
    
    private BoardModel[] _boards;
    private LevelModel[] _levels;
    private RulesSettingsModel _rulesSettings = new();
    
    internal UnityAction<RulesSettingsModel> onInputRulesSettings;
    internal event UnityAction onInputClosePanel
    {
        add => _panel.onInputClose += value;
        remove => _panel.onInputClose -= value;
    }

    internal void OutputPanel(bool value) =>
        _panel.OutputOpen(value);

    internal void OutputDatabase(BoardModel[] boards, LevelModel[] levels)
    {
        _boards = boards;
        _boardsToggles.OutputToggles(Array.ConvertAll(_boards, board => board.Icon));

        _levels = levels;
        _levelsToggles.OutputToggles(Array.ConvertAll(_levels, level => level.Icon));
    }

    private void Awake()
    {
        _boardsToggles.onInput += InputBoard;
        _levelsToggles.onInput += InputLevels;
    }
    private void OnDestroy()
    {
        _boardsToggles.onInput -= InputBoard;
        _levelsToggles.onInput -= InputLevels;
    }

    private void InputBoard(int index)
    {
        _rulesSettings.board = _boards[index];
        onInputRulesSettings.Invoke(_rulesSettings);
    }

    private void InputLevels(int index)
    {
        _rulesSettings.level = _levels[index];
        onInputRulesSettings.Invoke(_rulesSettings);
    }

    internal void OutputRules(RulesModel rules)
    {
        _rulesSettings.board = rules.board;
        _boardsToggles.OutputIsToggled(Array.IndexOf(_boards, _rulesSettings.board));
        
        _rulesSettings.level = rules.levels;
        _levelsToggles.OutputIsToggled(Array.IndexOf(_levels, _rulesSettings.level));
    }
}
