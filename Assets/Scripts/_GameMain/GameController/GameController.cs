using System;

public class GameController
{
    private GameBoardController _boardController = new();
    private GamePlayersController _playersController = new();
    private GamePlayersQueueController _playersQueueController = new();
    private GamePlayerLogicController _currentPlayerController = new();

    public Action<GameModel> onChanged;

    public void LoadProgress(ProgressModel progress)
    {
        _boardController.Load(progress.rules.board.SquereTilesCount);
        GameBoardModel board = _boardController.Get();
        
        _playersController.Set(progress.rules.levels.Points, progress.players);
        GamePlayersModel players = _playersController.Get();
        
        _playersQueueController.Set(players.gamePlayers);
        PlayerModel[] playersQueue = _playersQueueController.Get();
        
        onChanged.Invoke(new GameModel { board = board, players = players, playersQueue = playersQueue });


        ///*
        while (board.IsInteractable && playersQueue[0].logic.Logic != LogicModel.LogicType.Player)
        {
            PlayerModel player = playersQueue[0];
            (int x, int y) index = _currentPlayerController.GetIndex(board, player);
            
            _boardController.SetTile(index, player);
            board = _boardController.Get();
            
            if (board.IsInteractable)
                _playersQueueController.SetNext();

            _playersController.SetPoints(board.Winner);
            players = _playersController.Get();

            playersQueue = _playersQueueController.Get();

            onChanged.Invoke(new GameModel { board = board, players = players, playersQueue = playersQueue });
        }
        //*/
    }

    public void InputRestart()
    {
        _boardController.Restart();
        GameBoardModel board = _boardController.Get();

        GamePlayersModel players = _playersController.Get();
        
        _playersQueueController.Set(players.gamePlayers);
        PlayerModel[] playersQueue = _playersQueueController.Get();

        onChanged.Invoke(new GameModel { board = board, players = players, playersQueue = playersQueue });


        ///*
        while (board.IsInteractable && playersQueue[0].logic.Logic != LogicModel.LogicType.Player)
        {
            PlayerModel player = playersQueue[0];
            (int x, int y) index = _currentPlayerController.GetIndex(board, player);

            _boardController.SetTile(index, player);
            board = _boardController.Get();

            if (board.IsInteractable)
                _playersQueueController.SetNext();

            _playersController.SetPoints(board.Winner);
            players = _playersController.Get();

            playersQueue = _playersQueueController.Get();

            onChanged.Invoke(new GameModel { board = board, players = players, playersQueue = playersQueue });
        }
        //*/
    }

    public void InputTile((int x, int y) index)
    {
        PlayerModel player = _playersQueueController.Get()[0];

        bool moveIsDone = _boardController.SetTile(index, player);
        GameBoardModel board = _boardController.Get();

        _playersController.SetPoints(board.Winner);
        GamePlayersModel players = _playersController.Get();

        if (moveIsDone && board.IsInteractable)
            _playersQueueController.SetNext();
        PlayerModel[] playersQueue = _playersQueueController.Get();

        onChanged.Invoke(new GameModel { board = board, players = players, playersQueue = playersQueue });


        ///*
        while (board.IsInteractable && playersQueue[0].logic.Logic != LogicModel.LogicType.Player)
        {
            player = playersQueue[0];
            index = _currentPlayerController.GetIndex(board, player);

            _boardController.SetTile(index, player);
            board = _boardController.Get();

            if (board.IsInteractable)
                _playersQueueController.SetNext();

            _playersController.SetPoints(board.Winner);
            players = _playersController.Get();

            playersQueue = _playersQueueController.Get();

            onChanged.Invoke(new GameModel { board = board, players = players, playersQueue = playersQueue });
        }
        //*/
    }
}