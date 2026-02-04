using System;
using System.Collections.Generic;
using UnityEngine;

internal class GameBoardController
{
    private System.Random _random = new();
    private TilesController _tilesController = new();
    private LineController _lineController = new();
    private WinnerController _winnerController = new();
    private IsInteractableController _isInteractableController = new();

    //private TileModel[,] _tiles;
    //private LineModel[] _lines;
    //private PlayerModel _winner;
    //private bool _isInteractable;

    //private PlayerModel _player;
    
    internal Action<GameBoardModel> onChanged;

    public void Load(int tilesSqCount)
    {
        _tilesController.Load(tilesSqCount);
        TileModel[,] tiles = _tilesController.Get();

        _lineController.Set(tiles);
        LineModel[] lines = _lineController.Get();

        _winnerController.Set(lines);
        PlayerModel winner = _winnerController.Get();

        _isInteractableController.Set(tiles, winner);
        bool isInteractable = _isInteractableController.Get();

        onChanged.Invoke(new GameBoardModel(tiles, lines, winner, isInteractable));
    }

    internal void SetPlayer(PlayerModel player)
    {
        /*
        if (_player.logic.Logic == LogicModel.LogicType.Player || !_isInteractable) return;

        Dictionary<(int x, int y), int> tilesPoints = new();
        for (int x = 0; x < _tiles.GetLength(0); x++)
            for (int y = 0; y < _tiles.GetLength(0); y++)
                if (_tiles[x, y].player == null)
                    tilesPoints.Add(new (x,y), 0);
        
        foreach (var line in _lines)
        {
            if (Array.Exists(line.tilesIndex, i => _tiles[i.x, i.y].player != null))
            {
                (int x, int y) onePlayerTileIndex = Array.Find(line.tilesIndex, i => _tiles[i.x, i.y].player != null);
                PlayerModel onePlayer = _tiles[onePlayerTileIndex.x, onePlayerTileIndex.y].player;
                if (!Array.Exists(line.tilesIndex, i => _tiles[i.x, i.y].player != null && _tiles[i.x, i.y].player != onePlayer))
                {
                    if (onePlayer == _player)
                    {
                        (int x, int y)[] winTilesIndexes = Array.FindAll(line.tilesIndex, i => _tiles[i.x, i.y].player == null);
                        Array.ForEach(winTilesIndexes, i => tilesPoints[i] += (line.tilesIndex.Length - winTilesIndexes.Length + 3));
                    }
                    else
                    {
                        (int x, int y)[] loseTilesIndexes = Array.FindAll(line.tilesIndex, i => _tiles[i.x, i.y].player == null);
                        Array.ForEach(loseTilesIndexes, i => tilesPoints[i] += (line.tilesIndex.Length - loseTilesIndexes.Length + 2));
                    }
                }
            }
            else
            {
                Array.ForEach(line.tilesIndex, i => tilesPoints[i] += 1);
            }
        }

        int maxPoints = 0;
        foreach (var item in tilesPoints)
            if (item.Value > maxPoints)
                maxPoints = item.Value;

        List<(int x, int y)> tiles = new();
        foreach (var item in tilesPoints)
            if (item.Value == maxPoints)
                tiles.Add(item.Key);

        (int x, int y) tileIndex = tiles[_random.Next(tiles.Count)];
        _tilesController.FillTile(tileIndex, _player);

        return;
        */
    }

    internal void Restart()
    {
        _tilesController.ClearTiles();
        TileModel[,] tiles = _tilesController.Get();

        _lineController.Set(tiles);
        LineModel[] lines = _lineController.Get();

        _winnerController.Set(lines);
        PlayerModel winner = _winnerController.Get();

        _isInteractableController.Set(tiles, winner);
        bool isInteractable = _isInteractableController.Get();

        onChanged.Invoke(new GameBoardModel(tiles, lines, winner, isInteractable));
    }

    internal void SetTile(Vector2Int index, PlayerModel player)
    {
        _tilesController.FillTile(new(index.x, index.y), player);
        TileModel[,] tiles = _tilesController.Get();

        _lineController.Set(tiles);
        LineModel[] lines = _lineController.Get();

        _winnerController.Set(lines);
        PlayerModel winner = _winnerController.Get();

        _isInteractableController.Set(tiles, winner);
        bool isInteractable = _isInteractableController.Get();

        onChanged.Invoke(new GameBoardModel(tiles, lines, winner, isInteractable));
    }
}