using System;

internal class RulesController
{
    private RulesModel _rules = new();

    internal Action onInputSaveProgress;
    internal Action<RulesModel> onRulesModelChanged;

    internal void GetRules(ref RulesModel rules)
    {
        rules = _rules;
    }

    internal void SetRules(RulesModel rules)
    {
        _rules.levels = rules.levels;
        _rules.board = rules.board;

        onRulesModelChanged.Invoke(_rules);
    }

    internal void SetRulesSettings(RulesSettingsModel rulesSettings)
    {
        _rules.levels = rulesSettings.level;
        _rules.board = rulesSettings.board;

        onRulesModelChanged.Invoke(_rules);
        onInputSaveProgress.Invoke();
    }
}