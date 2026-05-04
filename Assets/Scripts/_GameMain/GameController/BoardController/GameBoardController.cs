using System;
using UnityEngine;

internal class GameBoardController
{
    private const int LineLength = 3;
    private TilesController _tilesController = new();
    private LineController _lineController = new();
    private WinnerController _winnerController = new();
    private IsInteractableController _isInteractableController = new();

    internal Action<GameBoardModel> onChanged;

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

        onChanged.Invoke(new GameBoardModel(tiles, lines, winner, isInteractable));
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

        onChanged.Invoke(new GameBoardModel(tiles, lines, winner, isInteractable));
    }

    internal void SetTile(Vector2Int index, PlayerModel player)
    {
        _tilesController.FillTile(new(index.x, index.y), player);
        TileModel[,] tiles = _tilesController.Get();

        _lineController.SetShort(tiles, LineLength);
        LineModel[] lines = _lineController.Get();

        _winnerController.Set(lines);
        PlayerModel winner = _winnerController.Get();

        _isInteractableController.Set(winner, lines);
        bool isInteractable = _isInteractableController.Get();

        onChanged.Invoke(new GameBoardModel(tiles, lines, winner, isInteractable));
    }
}