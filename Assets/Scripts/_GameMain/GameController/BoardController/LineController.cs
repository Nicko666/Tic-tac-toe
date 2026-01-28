using System;

internal class LineController
{
    LineModel[] _lines = new LineModel[0];
    
    internal LineModel[] SetLines(TileModel[,] tiles)
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