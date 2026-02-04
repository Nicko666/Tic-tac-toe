using System;
using System.Collections.Generic;
using UnityEngine;

internal class GamePlayerLogicController
{
    private System.Random _random = new();
    private GameBoardModel _board;
    private PlayerModel _player;

    internal Action<Vector2Int, PlayerModel> onInput;

    internal void SetBoard(GameBoardModel board) =>
        _board = board;

    internal void InputTile(Vector2Int tileIndex) =>
        onInput.Invoke(tileIndex, _player);

    internal void SetPlayer(GamePlayerModel[] players)
    {
        _player = players[0].player;

        if (_player.logic.Logic == LogicModel.LogicType.Player) return;

        if (!_board.IsInteractable) return;

        Dictionary<(int x, int y), int> tilesPoints = new();
        for (int x = 0; x < _board.Tiles.GetLength(0); x++)
            for (int y = 0; y < _board.Tiles.GetLength(0); y++)
                if (_board.Tiles[x, y].player == null)
                    tilesPoints.Add(new(x, y), 0);

        foreach (var line in _board.Lines)
        {
            if (Array.Exists(line.tilesIndex, i => _board.Tiles[i.x, i.y].player != null))
            {
                (int x, int y) onePlayerTileIndex = Array.Find(line.tilesIndex, i => _board.Tiles[i.x, i.y].player != null);
                PlayerModel onePlayer = _board.Tiles[onePlayerTileIndex.x, onePlayerTileIndex.y].player;
                if (!Array.Exists(line.tilesIndex, i => _board.Tiles[i.x, i.y].player != null && _board.Tiles[i.x, i.y].player != onePlayer))
                {
                    if (onePlayer == _player)
                    {
                        (int x, int y)[] winTilesIndexes = Array.FindAll(line.tilesIndex, i => _board.Tiles[i.x, i.y].player == null);
                        Array.ForEach(winTilesIndexes, i => tilesPoints[i] += (line.tilesIndex.Length - winTilesIndexes.Length + 3));
                    }
                    else
                    {
                        (int x, int y)[] loseTilesIndexes = Array.FindAll(line.tilesIndex, i => _board.Tiles[i.x, i.y].player == null);
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
        
        onInput.Invoke(new Vector2Int(tileIndex.x, tileIndex.y), _player);
    }
}