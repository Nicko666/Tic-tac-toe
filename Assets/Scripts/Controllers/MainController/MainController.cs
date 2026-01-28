using System;

public class MainController
{
    private PlayersController _playersController = new();
    private RulesController _rulesController = new();

    public event Action<RulesModel> onRulesChanged
    {
        add => _rulesController.onRulesModelChanged += value;
        remove => _rulesController.onRulesModelChanged -= value;
    }
    public event Action<PlayerModel[]> onPlayersChanged
    {
        add => _playersController.onPlayersChanged += value;
        remove => _playersController.onPlayersChanged -= value;
    }

    public void SetRules(RulesModel rules) => _rulesController.SetRules(rules);
    
    public void SetPlayers(PlayerModel[] playerModels) => _playersController.SetPlayers(playerModels);
    
    internal void SetDatabase(int minPlayersCount, LogicModel[] defaultLogics, MarkModel[] defaultMarks)
    {
        _playersController.SetDatabase(minPlayersCount, defaultLogics, defaultMarks);
    }
}
