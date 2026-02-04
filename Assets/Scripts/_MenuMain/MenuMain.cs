using System;
using UnityEngine;

[Serializable]
internal class MenuMain : IMain
{
    private MenuPlayersController _playersController = new();
    private MenuRulesController _rulesController = new();
    [SerializeField] private MenuPresenter _menuPresenter;

    internal override event Action<SoundModel> onInputSound
    {
        add => _menuPresenter.onInputSound += value;
        remove => _menuPresenter.onInputSound -= value;
    }
    internal override event Action<SceneModel> onInputScene
    {
        add => _menuPresenter.onInputGameScene += value;
        remove => _menuPresenter.onInputGameScene -= value;
    }
    internal override event Action<SettingsOutputModel> onInputSettings
    {
        add => _menuPresenter.onInputSettings += value;
        remove => _menuPresenter.onInputSettings -= value;
    }
    internal override event Action onInputSaveProgress
    {
        add
        {
            _playersController.onInputSaveProgress += value;
            _rulesController.onInputSaveProgress += value;
        }
        remove
        {
            _playersController.onInputSaveProgress -= value;
            _rulesController.onInputSaveProgress -= value;
        }
    }

    internal override void OutputProgressDatabase(ProgressDatabase database)
    {
        _playersController.SetDatabase(database.MinPlayersCount, database.Logics, database.Marks);
        _menuPresenter.OutputProgressDatabase(database.Marks, database.Logics, database.Boards, database.Levels);
    }

    internal override void OutputFrameIntervals(FrameIntervalModel[] frameIntervals) =>
        _menuPresenter.OutputFrameIntervals(frameIntervals);

    internal override void OutputPlayers(PlayerModel[] players) =>
        _playersController.SetPlayers(players);

    internal override void OutputRules(RulesModel rules) =>
        _rulesController.SetRules(rules);
        
    internal override void OutputSettings(SettingsOutputModel settings) =>
        _menuPresenter.OutputSettings(settings);

    internal override void OutputLoadedProgressData(ProgressData data)
    {
        
    }

    internal override void OutputSavedProgressData(ref ProgressData data)
    {

        RulesModel rules = new();
        _rulesController.GetRules(ref rules);
        if (rules.levels != null) data.levelsID = rules.levels.ID;
        if (rules.board != null) data.boardID = rules.board.ID;
        
        PlayerModel[] players = new PlayerModel[0];
        _playersController.GetPlayers(ref players);
        data.playersData = Array.ConvertAll(players, player =>
        {
            PlayerData playerData = new();
            if (player.logic != null) playerData.logicID = player.logic.ID;
            if (player.mark != null) playerData.markID = player.mark.ID;
            playerData.hue = player.hue;
            playerData.saturation = player.saturation;
            return playerData;
        });
    }

    private void Awake()
    {
        _menuPresenter.onInputAddPlayer += _playersController.AddPlayer;
        _menuPresenter.onInputRemoveLastPlayer += _playersController.RemoveLastPlayer;
        _menuPresenter.onInputRemovePlayer += _playersController.RemovePlayer;
        _menuPresenter.onInputPlayerIndex += _playersController.SetPlayerIndex;
        _menuPresenter.onInputPlayerSettings += _playersController.SetPlayerSettings;
        _menuPresenter.onInputRulesSettings += _rulesController.SetRulesSettings;
        _playersController.onChanged += _menuPresenter.OutputPlayers;
        _rulesController.onChanged += _menuPresenter.OutputRules;
    }
}