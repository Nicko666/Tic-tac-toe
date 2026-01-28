using System;
using UnityEngine;

public class GameController
{
    private GamePlayersController _playersController = new();
    private GameBoardController _boardController = new();
    private GamePlayersQueueController _playersQueueController = new();

    public Action<GameBoardModel> onBoardChanged;
    public Action<GamePlayersModel> onPlayersChanged;
    public Action<GamePlayerModel[]> onPlayersQueueChanged;

    public GameController()
    {
        _playersController.onChanged += OutputPlayersChanged;
        _playersQueueController.onChanged += OutputPlayersQueueChanged;
        _boardController.onChanged += OutputBoardChanged;
    }

    public void LoadRules(in RulesModel rules)
    {
        _playersController.LoadMaxPoints(rules.levels.Points);

        _boardController.Load(rules.board.SquereTilesCount);
        _boardController.Restart();
    }

    public void LoadPlayers(in PlayerModel[] players)
    {
        _playersController.LoadGamePlayers(players);

        _playersQueueController.Restart();
    }

    public void InputRestart()
    {
        _boardController.Restart();

        _playersQueueController.Restart();
    }

    public void InputTile(Vector2Int tileIndex)
    {
        _boardController.SetTile(tileIndex);
    }

    private void OutputPlayersChanged(GamePlayersModel gamePlayers)
    {
        onPlayersChanged.Invoke(gamePlayers);
        
        _playersQueueController.SetDefault(gamePlayers.gamePlayers);
    }

    private void OutputBoardChanged(GameBoardModel gameBoard)
    {
        onBoardChanged.Invoke(gameBoard);
        
        if (gameBoard.Winner != null)
            _playersController.AddPoint(gameBoard.Winner);
        
        if (gameBoard.IsInteractable)
            _playersQueueController.Next();
    }

    private void OutputPlayersQueueChanged(GamePlayerModel[] gamePlayers)
    {
        onPlayersQueueChanged.Invoke(gamePlayers);

        _boardController.SetPlayer(gamePlayers[0].player);
    }
}