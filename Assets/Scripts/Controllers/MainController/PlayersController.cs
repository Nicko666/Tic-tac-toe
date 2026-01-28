using System;
using System.Collections.Generic;

internal class PlayersController
{
    private int _minPlayersCount;
    private readonly List<PlayerModel> _players = new();
    private PlayerPool _playerPool;

    internal Action<PlayerModel[]> onPlayersChanged;

    internal void SetDatabase(int minPlayersCount, LogicModel[] defaultLogics, MarkModel[] defaultMarks)
    {
        _minPlayersCount = minPlayersCount;
        _playerPool = new(defaultLogics, defaultMarks);
    }

    internal void SetPlayers(PlayerModel[] players)
    {
        _players.Clear();
        _players.AddRange(players);

        _players.ForEach(player => _playerPool.GetPlayer(ref player));

        while (_players.Count < _minPlayersCount)
            _players.Add(_playerPool.GetPlayer());

        onPlayersChanged.Invoke(_players.ToArray());
    }

    class PlayerPool
    {
        private Random _random = new Random();
        private LogicModel[] _defaultLogics;
        private MarkModel[] _defaultMarks;

        internal PlayerPool(LogicModel[] defaultLogics, MarkModel[] defaultMarks)
        {
            _defaultLogics = defaultLogics;
            _defaultMarks = defaultMarks;
        }

        internal PlayerModel GetPlayer()
        {
            PlayerModel player = new();
            player.logic = _defaultLogics[_random.Next(_defaultLogics.Length)];
            player.mark = _defaultMarks[_random.Next(_defaultMarks.Length)];
            player.hue = (float)_random.Next(0, 100) / 100;
            player.saturation = (float)_random.Next(0, 100) / 100;
            return player;
        }

        internal void GetPlayer(ref PlayerModel player)
        {
            player.logic ??= _defaultLogics[0];
            player.mark ??= _defaultMarks[0];
        }
    }
}