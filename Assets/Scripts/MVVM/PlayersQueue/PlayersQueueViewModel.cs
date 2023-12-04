using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PlayersQueueViewModel
{
    protected PlayersQueueModel _model;

    public ReactiveCollection<PlayerViewModel> playersViewModels = new();
    public ReactiveProperty<bool> playersMin = new();
    public ReactiveProperty<bool> playersMax = new();
    int minCount;
    int maxCount;

    public PlayerViewModel FirstPlayer => playersViewModels.Value.First();
    public PlayerViewModel SecondPlayer => playersViewModels.Value[1];


    public Action<PlayerViewModel> onSelect;
    public Action onAddRequest;
    public Action onRemoved;


    public PlayersQueueViewModel(PlayersQueueModel model)
    {
        if (_model != null)
            ViewModelUnsubscribe();

        _model = model;

        if (_model != null)
            ViewModelSubscribe();

        ViewModelUpdate();

    }

    void ViewModelSubscribe()
    {
        _model.playerModels.onValueChanged += OutputPlayersValueChanged;
        _model.playerModels.onAdd += OutputAdd;
        _model.playerModels.onMove += OutputMove;
        _model.playerModels.onRemove += OutputRemove;
    }
    void ViewModelUnsubscribe()
    {
        _model.playerModels.onValueChanged -= OutputPlayersValueChanged;
        _model.playerModels.onAdd -= OutputAdd;
        _model.playerModels.onMove -= OutputMove;
        _model.playerModels.onRemove -= OutputRemove;
    }
    void ViewModelUpdate()
    {
        if (_model != null)
        {
            OutputPlayersValueChanged(_model.playerModels.Value ?? null);
            OutputMinValueChanged(_model.minCount.Value);
            OutputMaxValueChanged(_model.maxCount.Value);
        }
        else
        {
            OutputPlayersValueChanged(null);
        }
    }

    void OutputPlayersValueChanged(ObservableCollection<PlayerModel> value)
    {
        playersViewModels.Value = new();
        if (value != null)
            foreach (var playerModel in value)
                OutputAdd(playerModel);
    }
    void OutputAdd(PlayerModel playerModel)
    {
        PlayerViewModel playerViewModel = new PlayerViewModel(playerModel);
        playersViewModels.Add(playerViewModel);
        playerViewModel.onSelected += RequestSelectPlayer;

        OutputMinValue();
        OutputMaxValue();
    }
    void OutputMove(int oldIndex, int newIndex)
    {
        playersViewModels.Move(oldIndex, newIndex);
    }
    void OutputRemove(PlayerModel playerModel)
    {
        while (playersViewModels.Value.Any(playerViewModel => playerViewModel.Model == playerModel))
        {
            var playerViewModel = playersViewModels.Value.First(playerViewModel => playerViewModel.Model == playerModel);
            playersViewModels.Remove(playerViewModel);
            playerViewModel.onSelected -= RequestSelectPlayer;
        }

        OutputMinValue();
        OutputMaxValue();
    }
    void OutputMinValueChanged(int count)
    {
        minCount = count;
        OutputMinValue();
    }
    void OutputMaxValueChanged(int count)
    {
        maxCount = count;
        OutputMaxValue();
    }
    void OutputMinValue()
    {
        playersMin.Value = (minCount < playersViewModels.Value.Count);
    }
    void OutputMaxValue()
    {
        playersMax.Value = (maxCount > playersViewModels.Value.Count);
    }

    public void RequestAddPlayer()
    {
        onAddRequest?.Invoke();
    }
    public void RequestMovePlayer(int oldValue, int newValue)
    {
        InputMovePlayer(oldValue, newValue);
    }
    public void RequestRemovePlayer()
    {
        InputRemovePlayer(_model.playerModels.Value.Last());
    }
    public void RequestSelectPlayer(PlayerViewModel playerViewModel)
    {
        onSelect?.Invoke(playerViewModel);
    }
    
    public void InputAddPlayer(PlayerModel playerModel)
    {
        //if (maxCount > _model.playerModels.Value.Count)
            _model.playerModels.Add(playerModel);
        
    }
    public void InputMovePlayer(int oldValue, int newValue)
    {
        _model.playerModels.Move(oldValue, newValue);

    }
    public void InputRemovePlayer(PlayerModel playerModel)
    {
        //if (minCount < _model.playerModels.Value.Count)
            _model.playerModels.Remove(playerModel);
        onRemoved?.Invoke();
        
    }


}
