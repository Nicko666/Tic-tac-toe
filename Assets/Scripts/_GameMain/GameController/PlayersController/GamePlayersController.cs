using System;

public class GamePlayersController
{
    private int _maxPoints;
    private GamePlayersModel _gamePlayers = new(new GamePlayerModel[0]);

    internal Action<GamePlayersModel> onChanged;

    internal void LoadProgress(ProgressModel progress)
    {
        _maxPoints = progress.rules.levels.Points;
        _gamePlayers.gamePlayers = Array.ConvertAll(progress.players, player => new GamePlayerModel(player));

        onChanged?.Invoke(_gamePlayers);
    }

    internal void SetPoints(PlayerModel winner)
    {
        if (!Array.Exists(_gamePlayers.gamePlayers, i => i.player == winner)) return;

        int gamePlayerIndex = Array.FindIndex(_gamePlayers.gamePlayers, i => i.player == winner);
        _gamePlayers.gamePlayers[gamePlayerIndex].points++;

        if (Array.Exists(_gamePlayers.gamePlayers, gamePlayer => gamePlayer.points >= _maxPoints))
            _gamePlayers.winner = Array.Find(_gamePlayers.gamePlayers, gamePlayer => gamePlayer.points >= _maxPoints).player;

        onChanged?.Invoke(_gamePlayers);
    }
}