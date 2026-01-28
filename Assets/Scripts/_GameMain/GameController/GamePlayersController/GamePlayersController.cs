using System;

public class GamePlayersController
{
    private int _maxPoints;
    private GamePlayersModel _gamePlayers = new(new GamePlayerModel[0]);

    internal Action<GamePlayersModel> onChanged;
    
    internal void LoadMaxPoints(int points)
    {
        _maxPoints = points;
    }

    internal void LoadGamePlayers(PlayerModel[] players)
    {
        _gamePlayers.gamePlayers = Array.ConvertAll(players, player => new GamePlayerModel(player));

        onChanged?.Invoke(_gamePlayers);
    }

    internal void AddPoint(PlayerModel winner)
    {
        if (!Array.Exists(_gamePlayers.gamePlayers, i => i.player == winner)) return;

        int gamePlayerIndex = Array.FindIndex(_gamePlayers.gamePlayers, i => i.player == winner);
        _gamePlayers.gamePlayers[gamePlayerIndex].points++;

        if (Array.Exists(_gamePlayers.gamePlayers, gamePlayer => gamePlayer.points >= _maxPoints))
            _gamePlayers.winner = Array.Find(_gamePlayers.gamePlayers, gamePlayer => gamePlayer.points >= _maxPoints).player;

        onChanged?.Invoke(_gamePlayers);
    }





    /*
    internal void SetBoardFilled(GameBoardModel gameBoard)
    {
        if (gameBoard.winner == null)
        {
            if (gameBoard.isInteractable)
                onPointMissed.Invoke(_gamePlayers);
        }
        else
        {
            int gamePlayerIndex = Array.FindIndex(_gamePlayers.gamePlayers, i => i.player == gameBoard.winner);
            _gamePlayers.gamePlayers[gamePlayerIndex].points++;

            if (Array.Exists(_gamePlayers.gamePlayers, gamePlayer => gamePlayer.points >= _winPoints))
                _gamePlayers.winner = Array.Find(_gamePlayers.gamePlayers, gamePlayer => gamePlayer.points >= _winPoints).player;

            onPointAdded.Invoke(_gamePlayers);
        }
    }*/
}