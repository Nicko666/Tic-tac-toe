using System;
using System.Collections.Generic;

internal class LineController
{
    private readonly List<LineModel> _lines = new ();

    internal LineModel[] Get() => 
        _lines.ToArray();

    internal void SetShort(TileModel[,] tiles, int lineLength)
    {
        _lines.Clear();

        int lengthX = tiles.GetLength(0);
        int lengthY = tiles.GetLength(1);
    
        int lineOffset = lineLength - 1;

        for (int x = 0; x < lengthX; x++)
            for (int y = 0; y < lengthY - lineOffset; y++)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Vertical;
                line.tilesIndex = new (int, int)[lineLength];

                for (int tile = 0; tile < lineLength; tile++)
                    line.tilesIndex[tile] = new(x, y + tile);

                _lines.Add(line);
            }

        for (int y = 0; y < lengthY; y++)
            for (int x = 0; x < lengthX - lineOffset; x++)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Horizontal;
                line.tilesIndex = new (int, int)[lineLength];

                for (int tile = 0; tile < lineLength; tile++)
                    line.tilesIndex[tile] = new(x + tile, y);

                _lines.Add(line);
            }

        for (int x = 0; x < lengthX - lineOffset; x++)
            for (int y = 0; y < lengthY - lineOffset; y++)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Up;
                line.tilesIndex = new (int, int)[lineLength];

                for (int tile = 0; tile < lineLength; tile++)
                    line.tilesIndex[tile] = new(x + tile, y + tile);

                _lines.Add(line);
            }

        for (int x = 0; x < lengthX - lineOffset; x++)
            for (int y = lengthY - 1; y > lineOffset - 1; y--)
            {
                LineModel line = new();
                line.direction = LineModel.DirectionType.Down;
                line.tilesIndex = new (int, int)[lineLength];

                for (int tile = 0; tile < lineLength; tile++)
                    line.tilesIndex[tile] = new(x + tile, y - tile);

                _lines.Add(line);
            }

        for (int i = 0; i < _lines.Count; i++)
        {
            LineModel line = _lines[i];
            
            if (Array.Exists(line.tilesIndex, index => tiles[index.x, index.y].player == null))
            {
                List<PlayerModel> players = new();

                Array.ForEach(line.tilesIndex, index =>
                {
                    PlayerModel player = tiles[index.x, index.y].player;
                    if (player != null && !players.Contains(player))
                        players.Add(player);
                });

                line.hasWinTiles = players.Count < 2;
            }
            else
                line.hasWinTiles = false;


            (int x, int y) firstTileIndex = line.tilesIndex[0];

            bool win = Array.TrueForAll(line.tilesIndex, currentTilesIndex =>
            {
                TileModel tile = tiles[currentTilesIndex.x, currentTilesIndex.y];
                bool isWinned = tile.player != null && tile.player == tiles[firstTileIndex.x, firstTileIndex.y].player;
                return isWinned;
            });

            line.winner = win ? tiles[firstTileIndex.x, firstTileIndex.y].player : null;
            _lines[i] = line;
        }
    }
}