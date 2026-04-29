using System;
using System.Collections.Generic;
using UnityEngine;

internal class LineController
{
    LineModel[] _lines = new LineModel[0];

    internal LineModel[] Get() => 
        _lines;

    private const int LineLength = 3; // cannot be less then 1

    internal LineModel[] SetShort(TileModel[,] tiles)
    {
        List<LineModel> lines = new List<LineModel>();
        int lengthX = tiles.GetLength(0);
        int lengthY = tiles.GetLength(1);
    
        int lineOffset = LineLength - 1;

        for (int x = 0; x < lengthX; x++)
            for (int y = 0; y < lengthY - lineOffset; y++)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Vertical;
                line.tilesIndex = new (int, int)[LineLength];

                for (int tile = 0; tile < LineLength; tile++)
                    line.tilesIndex[tile] = new(x, y + tile);

                lines.Add(line);
                Debug.Log($"{line.direction.ToString()} line added");
            }

        for (int y = 0; y < lengthY; y++)
            for (int x = 0; x < lengthX - lineOffset; x++)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Horizontal;
                line.tilesIndex = new (int, int)[LineLength];

                for (int tile = 0; tile < LineLength; tile++)
                    line.tilesIndex[tile] = new(x + tile, y);

                lines.Add(line);
                Debug.Log($"{line.direction.ToString()} line added");
            }

        for (int x = 0; x < lengthX - lineOffset; x++)
            for (int y = 0; y < lengthY - lineOffset; y++)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Up;
                line.tilesIndex = new (int, int)[LineLength];

                for (int tile = 0; tile < LineLength; tile++)
                    line.tilesIndex[tile] = new(x + tile, y + tile);

                lines.Add(line);
                Debug.Log($"{line.direction.ToString()} line added");
            }

        for (int x = 0; x < lengthX - lineOffset; x++)
            for (int y = lengthY - 1; y > lineOffset - 1; y--)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Down;
                line.tilesIndex = new (int, int)[LineLength];

                for (int tile = 0; tile < LineLength; tile++)
                    line.tilesIndex[tile] = new(x + tile, y - tile);

                lines.Add(line);
                Debug.Log($"{line.direction.ToString()} line added");
            }

        _lines = lines.ToArray();

        Debug.Log($"Lines Count is: {_lines.Length}");

        for (int i = 0; i < _lines.Length; i++)
        {
            LineModel line = _lines[i];

            (int x, int y) firstTileIndex = line.tilesIndex[0];

            bool win = Array.TrueForAll(line.tilesIndex, currentTilesIndex =>
            {
                TileModel tile = tiles[currentTilesIndex.x, currentTilesIndex.y];
                bool isWinned = tile.player != null && tile.player == tiles[firstTileIndex.x, firstTileIndex.y].player;
                return isWinned;
            });

            _lines[i].winner = win ? tiles[firstTileIndex.x, firstTileIndex.y].player : null;
        }

        return _lines;
    }

    internal LineModel[] SetLong(TileModel[,] tiles)
    {
        int length = tiles.GetLength(0);
        _lines = new LineModel[length * 2 + 2];

        for (int x = 0; x < length; x++)
        {
            _lines[x].direction = LineModel.DirectionType.Vertical;
            _lines[x].tilesIndex = new (int, int)[length];

            _lines[x + length].direction = LineModel.DirectionType.Horizontal;
            _lines[x + length].tilesIndex = new (int, int)[length];

            for (int y = 0; y < length; y++)
            {
                _lines[x].tilesIndex[y] = new(x, y);
                _lines[x + length].tilesIndex[y] = new(y, x);
            }
        }

        _lines[length * 2].direction = LineModel.DirectionType.Up;
        _lines[length * 2].tilesIndex = new (int, int)[length];

        _lines[length * 2 + 1].direction = LineModel.DirectionType.Down;
        _lines[length * 2 + 1].tilesIndex = new (int, int)[length];

        for (int x = 0; x < length; x++)
        {
            _lines[length * 2].tilesIndex[x] = new(x, x);
            _lines[length * 2 + 1].tilesIndex[x] = new(length - 1 - x, x);
        }

        for (int i = 0; i < _lines.Length; i++)
        {
            LineModel line = _lines[i];

            (int x, int y) firstTileIndex = line.tilesIndex[0];

            bool win = Array.TrueForAll(line.tilesIndex, currentTilesIndex =>
            {
                TileModel tile = tiles[currentTilesIndex.x, currentTilesIndex.y];
                bool isWinned = tile.player != null && tile.player == tiles[firstTileIndex.x, firstTileIndex.y].player;
                return isWinned;
            });
            
            _lines[i].winner = win ? tiles[firstTileIndex.x, firstTileIndex.y].player : null;
        }

        return _lines;
    }
}