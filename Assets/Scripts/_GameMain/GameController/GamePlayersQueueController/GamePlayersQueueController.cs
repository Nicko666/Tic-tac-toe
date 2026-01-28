using System;
using System.Collections.Generic;

internal class GamePlayersQueueController
{
    private GamePlayerModel[] _default = new GamePlayerModel[0];

    private Queue<GamePlayerModel> _current = new();

    public Action<GamePlayerModel[]> onChanged;

    internal void SetDefault(GamePlayerModel[] gamePlayers) =>
        _default = gamePlayers;

    internal void Restart()
    {
        if (Array.Exists(_default, gamePlayer => gamePlayer.points != 0))
            Array.Sort(_default, (x, y) => x.points < y.points ? 0 : 1 );
        
        _current = new(_default);
        onChanged.Invoke(_current.ToArray());
    }

    internal void Next()
    {
        if (_current.Count < 1) return;

        _current.Enqueue(_current.Dequeue());
        onChanged.Invoke(_current.ToArray());
    }
}