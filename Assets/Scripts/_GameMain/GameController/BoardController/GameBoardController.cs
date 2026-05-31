internal class GameBoardController
{
    private const int LineLength = 3;
    private TilesController _tilesController = new();
    private LineController _lineController = new();
    private WinnerController _winnerController = new();
    private IsInteractableController _isInteractableController = new();

    public GameBoardModel Get() =>
        new GameBoardModel(_tilesController.Get(), _lineController.Get(), _winnerController.Get(), _isInteractableController.Get());

    public void Load(int tilesSqCount)
    {
        _tilesController.Load(tilesSqCount);
        TileModel[,] tiles = _tilesController.Get();

        _lineController.SetShort(tiles, LineLength);
        LineModel[] lines = _lineController.Get();

        _winnerController.Set(lines);
        PlayerModel winner = _winnerController.Get();

        _isInteractableController.Set(winner, lines);
        bool isInteractable = _isInteractableController.Get();
    }

    internal void Restart()
    {
        _tilesController.ClearTiles();
        TileModel[,] tiles = _tilesController.Get();

        _lineController.SetShort(tiles, LineLength);
        LineModel[] lines = _lineController.Get();

        _winnerController.Set(lines);
        PlayerModel winner = _winnerController.Get();

        _isInteractableController.Set(winner, lines);
        bool isInteractable = _isInteractableController.Get();
    }

    internal bool SetTile((int x, int y) index, PlayerModel player)
    {
        bool result = false;

        result = _tilesController.FillTile(index, player);
        TileModel[,] tiles = _tilesController.Get();

        _lineController.SetShort(tiles, LineLength);
        LineModel[] lines = _lineController.Get();

        _winnerController.Set(lines);
        PlayerModel winner = _winnerController.Get();

        _isInteractableController.Set(winner, lines);
        bool isInteractable = _isInteractableController.Get();

        return result;
    }
}