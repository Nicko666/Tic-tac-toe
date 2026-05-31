using System;
using System.Collections.Generic;

internal class GamePlayersQueueController
{
    private Queue<PlayerModel> _current = new();

    internal PlayerModel[] Get() =>
        _current.ToArray();

    internal void Set(GamePlayerModel[] gamePlayers)
    {
        Array.Sort(gamePlayers, (a, b) => a.points > b.points ? 1 : 0);
        _current = new(Array.ConvertAll(gamePlayers, i => i.player));
    }

    internal void SetNext() =>
        _current.Enqueue(_current.Dequeue());
}