using System;
using UnityEngine;

internal class GameBoardController
{
    private System.Random _random = new();
    private TilesController _tilesController = new();
    private LineController _lineController = new();
    private WinnerController _winnerController = new();
    private IsInteractableController _isInteractableController = new();

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