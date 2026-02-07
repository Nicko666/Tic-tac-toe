using System;
using UnityEngine;

public class GameController
{
    private GamePlayersController _playersController = new();
    private GameBoardController _boardController = new();
    private GamePlayersQueueController _playersQueueController = new();
    private GamePlayerController _playerController = new();

    public Action<GameBoardModel> onBoardChanged;
    public Action<GamePlayersModel> onPlayersChanged;
    public Action<GamePlayerModel[]> onPlayersQueueChanged;

    public GameController()
    {
        _playersController.onChanged += OutputPlayersChanged;
        _playersQueueController.onChanged += OutputPlayersQueueChanged;
        _playerController.onInputTile += OutputTile;
        _boardController.onChanged += OutputBoardChanged;
    }

    public void LoadRules(in RulesModel rules)
    {
        _playersController.LoadMaxPoints(rules.levels.Points);
        _boardController.Load(rules.board.SquereTilesCount);
    }

    public void LoadPlayers(in PlayerModel[] players)
    {
        _playersController.LoadPlayers(players);
    }

    public void InputRestart()
    {
        _boardController.Restart();
    }

    public void InputTile(Vector2Int tileIndex)
    {
        _playerController.InputTile(tileIndex);
    }

    private void OutputBoardChanged(GameBoardModel gameBoard)
    {
        onBoardChanged.Invoke(gameBoard);
        
        _playerController.SetBoard(gameBoard);

        _playersQueueController.SetNext(gameBoard);

        _playersController.SetPoints(gameBoard.Winner);
    }

    private void OutputPlayersChanged(GamePlayersModel gamePlayers)
    {
        onPlayersChanged.Invoke(gamePlayers);
        
        _playersQueueController.SetDefault(gamePlayers.gamePlayers);
    }

    private void OutputPlayersQueueChanged(GamePlayerModel[] gamePlayers)
    {
        onPlayersQueueChanged.Invoke(gamePlayers);

        _playerController.SetPlayer(gamePlayers);
    }

    private void OutputTile(Vector2Int index, PlayerModel player)
    {
        _boardController.SetTile(index, player);
    }
}