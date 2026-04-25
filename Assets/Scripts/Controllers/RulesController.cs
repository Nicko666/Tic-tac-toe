using System;

internal class RulesController
{
    private RulesModel _rules = new();
    private BoardModel[] _boards;
    private LevelModel[] _levels;

    internal Action onInputSaveProgress;
    internal Action<RulesModel> onRulesChanged;

    internal void SetDatabase(LevelModel[] levels, BoardModel[] boards)
    {
        _levels = levels;
        _boards = boards;
    }
 
    internal void GetRules(ref RulesModel rules)
    {
        rules = _rules;
    }

    internal void SetRules(RulesModel rules)
    {
        _rules.levels = rules.levels;
        _rules.board = rules.board;

        onRulesChanged.Invoke(_rules);
    }

    internal RulesModel Get(int levelsID, int boardID)
    {
        _rules.levels = Array.Find(_levels, i => i.ID == levelsID);
        _rules.board = Array.Find(_boards, i => i.ID == boardID);
        return _rules;
    }

    internal void SetRulesSettings(RulesSettingsModel rulesSettings)
    {
        _rules.levels = rulesSettings.level;
        _rules.board = rulesSettings.board;

        onRulesChanged.Invoke(_rules);
        onInputSaveProgress.Invoke();
    }

    internal void GetData(ref int levelsID, int boardID, RulesModel rules)
    {
        if (rules.levels != null) levelsID = rules.levels.ID;
        if (rules.board != null) boardID = rules.board.ID;
    }
}