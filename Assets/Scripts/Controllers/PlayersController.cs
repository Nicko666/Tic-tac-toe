using System;
using System.Collections.Generic;

internal class PlayersController
{
    private readonly List<PlayerModel> _players = new();
    private PlayerPool _playerPool;
    
    private int _minPlayersCount;
    private LogicModel[] _logics;
    private MarkModel[] _marks;

    internal Action<PlayerModel[]> onPlayersChanged;

    internal void SetDatabase(int minPlayersCount, LogicModel[] logics, MarkModel[] marks)
    {
        _minPlayersCount = minPlayersCount;
        _logics = logics;
        _marks = marks;
        _playerPool = new(logics, marks);
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

    public void SetPlayersData(PlayerData[] playersData)
    {
        PlayerModel[] players = Array.ConvertAll(playersData, playerData =>
        {
            PlayerModel player = new PlayerModel();
            player.logic = Array.Find(_logics, i => i.ID == playerData.logicID);
            player.mark = Array.Find(_marks, i => i.ID == playerData.markID);
            player.hue = playerData.hue;
            player.saturation = playerData.saturation;
            return player;
        });

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