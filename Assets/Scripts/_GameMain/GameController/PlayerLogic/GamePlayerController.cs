using System;
using System.Collections.Generic;
using UnityEngine;

internal class GamePlayerController
{
    private System.Random _random = new();
    private GameBoardModel _board;
    private PlayerModel _player;

    internal Action<Vector2Int, PlayerModel> onInputTile;

    internal void SetBoard(GameBoardModel board) =>
        _board = board;

    internal void InputTile(Vector2Int tileIndex)
    {
        if (_board.Tiles[tileIndex.x, tileIndex.y].player == null)
            onInputTile.Invoke(tileIndex, _player);
    }

    internal void SetPlayer(GamePlayerModel[] players)
    {
        _player = players[0].player;

        if (_player.logic.Logic == LogicModel.LogicType.Player) return;

        if (!_board.IsInteractable) return;

        Dictionary<(int x, int y), int> tilesPoints = new();

        Array.ForEach(_board.Lines, line => Array.ForEach(line.tilesIndex, tileIndex =>
        {
            TileModel tileModel = _board.Tiles[tileIndex.x, tileIndex.y];

            if (tileModel.player == null)
            {
                tilesPoints.TryAdd(tileIndex, 0);
                tilesPoints[tileIndex] += 1;

                (int x, int y)[] playerTiles = Array.FindAll(line.tilesIndex, i => _board.Tiles[i.x, i.y].player == _player);
                (int x, int y)[] enemiesTiles = Array.FindAll(line.tilesIndex, i => _board.Tiles[i.x, i.y].player != _player && _board.Tiles[i.x, i.y].player != null);

                if (enemiesTiles.Length < 1)
                {
                    tilesPoints[tileIndex] += playerTiles.Length;
                    tilesPoints[tileIndex] += (playerTiles.Length == line.tilesIndex.Length - 1) ? 3 : 0;
                }
                if (playerTiles.Length < 1 && enemiesTiles.Length > 0)
                    if (Array.TrueForAll(enemiesTiles, i => _board.Tiles[i.x, i.y].player == _board.Tiles[enemiesTiles[0].x, enemiesTiles[0].y].player))
                    {
                        tilesPoints[tileIndex] += enemiesTiles.Length;
                        tilesPoints[tileIndex] += (enemiesTiles.Length == line.tilesIndex.Length - 1) ? 2 : 0;
                    }
            }
        }));

        int maxPoints = 0;
        foreach (var item in tilesPoints)
            if (item.Value > maxPoints)
                maxPoints = item.Value;

        List<(int x, int y)> tiles = new();
        foreach (var item in tilesPoints)
            if (item.Value == maxPoints)
                tiles.Add(item.Key);

        (int x, int y) tileIndex = tiles[_random.Next(tiles.Count)];
        
        onInputTile.Invoke(new Vector2Int(tileIndex.x, tileIndex.y), _player);
    }
}