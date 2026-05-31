using System;
using System.Collections.Generic;

internal class GamePlayerLogicController
{
    private System.Random _random = new();

    internal (int x, int y) GetIndex(GameBoardModel board, PlayerModel player)
    {
        Dictionary<(int x, int y), int> tilesPoints = new();

        Array.ForEach(board.Lines, line => Array.ForEach(line.tilesIndex, tileIndex =>
        {
            TileModel tileModel = board.Tiles[tileIndex.x, tileIndex.y];

            if (tileModel.player == null)
            {
                tilesPoints.TryAdd(tileIndex, 0);
                tilesPoints[tileIndex] += 1;

                (int x, int y)[] playerTiles = Array.FindAll(line.tilesIndex, i => board.Tiles[i.x, i.y].player == player);
                (int x, int y)[] enemiesTiles = Array.FindAll(line.tilesIndex, i => board.Tiles[i.x, i.y].player != player && board.Tiles[i.x, i.y].player != null);

                if (enemiesTiles.Length < 1)
                {
                    tilesPoints[tileIndex] += playerTiles.Length;
                    tilesPoints[tileIndex] += (playerTiles.Length == line.tilesIndex.Length - 1) ? 3 : 0;
                }
                if (playerTiles.Length < 1 && enemiesTiles.Length > 0)
                    if (Array.TrueForAll(enemiesTiles, i => board.Tiles[i.x, i.y].player == board.Tiles[enemiesTiles[0].x, enemiesTiles[0].y].player))
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

        return tiles[_random.Next(tiles.Count)];
    }
}