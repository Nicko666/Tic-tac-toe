using System;
using System.Collections.Generic;

public class GamePlayersController
{
    private int _maxPoints;
    public List<GamePlayerModel> _gamePlayers = new();
    public PlayerModel _winner;

    internal void Set(int maxPoints, PlayerModel[] players)
    {
        _maxPoints = maxPoints;
        _gamePlayers = new(Array.ConvertAll(players, player => new GamePlayerModel(player)));
    }

    internal GamePlayersModel Get() => 
        new GamePlayersModel() { gamePlayers = _gamePlayers.ToArray(), winner = _winner };

    internal void SetPoints(PlayerModel winner)
    {
        if (!_gamePlayers.Exists(i => i.player == winner)) return;

        int gamePlayerIndex = _gamePlayers.FindIndex(i => i.player == winner);
        GamePlayerModel gamePlayer = _gamePlayers[gamePlayerIndex];
        gamePlayer.points++;
        _gamePlayers[gamePlayerIndex] = gamePlayer;

        if (_gamePlayers.Exists(gamePlayer => gamePlayer.points >= _maxPoints))
            _winner = _gamePlayers.Find(gamePlayer => gamePlayer.points >= _maxPoints).player;
    }
}