using System;
using UnityEngine;

internal class RulesPresenter : MonoBehaviour
{
    [SerializeField] private PanelView _panel;
    [SerializeField] private TogglesImageView _boardsToggles, _levelsToggles;
    
    private BoardModel[] _boards;
    private LevelModel[] _levels;
    private RulesSettingsModel _rulesSettings = new();
    
    internal Action<RulesSettingsModel> onInputRulesSettings;
    internal Action<SoundModel> onInputSound;
    internal Action onInputClosePanel;

    internal void OutputPanel(bool value) =>
        _panel.OutputOpen(value);

    internal void OutputDatabase(BoardModel[] boards, LevelModel[] levels)
    {
        _boards = boards;
        _boardsToggles.OutputToggles(Array.ConvertAll(_boards, board => board.Icon));

        _levels = levels;
        _levelsToggles.OutputToggles(Array.ConvertAll(_levels, level => level.Icon));
    }

    internal void OutputRules(RulesModel rules)
    {
        _rulesSettings.board = rules.board;
        _boardsToggles.OutputIsToggled(Array.IndexOf(_boards, _rulesSettings.board));
        
        _rulesSettings.level = rules.levels;
        _levelsToggles.OutputIsToggled(Array.IndexOf(_levels, _rulesSettings.level));
    }

    private void Awake()
    {
        _boardsToggles.onInput += InputBoard;
        _levelsToggles.onInput += InputLevels;
        _panel.onInputClose += InputClosePanel;
    }

    private void OnDestroy()
    {
        _boardsToggles.onInput -= InputBoard;
        _levelsToggles.onInput -= InputLevels;
        _panel.onInputClose -= InputClosePanel;
    }

    private void InputClosePanel()
    {
        onInputClosePanel.Invoke();
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputBoard(int index)
    {
        _rulesSettings.board = _boards[index];
        onInputRulesSettings.Invoke(_rulesSettings);
        onInputSound.Invoke(SoundModel.Accept);
    }

    private void InputLevels(int index)
    {
        _rulesSettings.level = _levels[index];
        onInputRulesSettings.Invoke(_rulesSettings);
        onInputSound.Invoke(SoundModel.Accept);
    }
}
