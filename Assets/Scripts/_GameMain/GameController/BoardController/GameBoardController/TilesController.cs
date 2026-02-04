internal class TilesController
{
    private TileModel[,] _tiles = new TileModel[0,0];

    internal TileModel[,] Get() =>
        _tiles;

    internal TileModel[,] Load(int squearCount) =>
        _tiles = new TileModel[squearCount, squearCount];

    internal TileModel[,] ClearTiles() =>
        _tiles = new TileModel[_tiles.GetLength(0), _tiles.GetLength(0)];

    internal void FillTile((int x, int y) index, PlayerModel playerModel)
    {
        if (_tiles[index.x, index.y].player != null) return;

        TileModel[,] oldTiles = _tiles;
        _tiles = new TileModel[_tiles.GetLength(0), _tiles.GetLength(0)];

        for (int x = 0; x < _tiles.GetLength(0); x++)
            for (int y = 0; y < _tiles.GetLength(1); y++)
                _tiles[x, y].player = (x == index.x && y == index.y) ? playerModel : oldTiles[x, y].player;
    }
}