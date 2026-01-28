using System;

public class MenuRulesController
{
    private RulesModel _rules = new();

    public Action onInputSaveProgress;
    public Action<RulesModel> onChanged;

    public void GetRules(ref RulesModel rules)
    {
        rules = _rules;
    }

    public void SetRules(RulesModel rules)
    {
        _rules.levels = rules.levels;
        _rules.board = rules.board;

        onChanged.Invoke(_rules);
    }

    public void SetRulesSettings(RulesSettingsModel rulesSettings)
    {
        _rules.levels = rulesSettings.level;
        _rules.board = rulesSettings.board;

        onChanged.Invoke(_rules);
        onInputSaveProgress.Invoke();
    }
}