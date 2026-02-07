using System;
using System.Collections.Generic;

internal class GamePlayersQueueController
{
    private GamePlayerModel[] _default = new GamePlayerModel[0];
    private Queue<GamePlayerModel> _current = new();
    private GameBoardModel _gameBoard;

    public Action<GamePlayerModel[]> onChanged;
    
    internal void SetDefault(GamePlayerModel[] gamePlayers)
    {
        _default = gamePlayers;

        if (_current.Count != gamePlayers.Length)
            _current = new(_default);

        onChanged.Invoke(_current.ToArray());
    }

    internal void SetNext(GameBoardModel gameBoard)
    {
        _gameBoard = gameBoard;

        if (!gameBoard.IsInteractable || _current.Count < 1) return;

        bool boardIsClear = true;
        foreach (TileModel tile in gameBoard.Tiles)
            if (tile.player != null)
            {
                boardIsClear = false;
                break;
            }
        
        if (boardIsClear)
        {
            if (Array.Exists(_default, gamePlayer => gamePlayer.points != 0))
                Array.Sort(_default, (x, y) => x.points < y.points ? 0 : 1);

            _current = new(_default);
        }
        else
            _current.Enqueue(_current.Dequeue());

        onChanged.Invoke(_current.ToArray());
    }
}