using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.SpaceFighter;

public class GameMain : MonoBehaviour
{
    //maxPoints
    //playersSorting
    //players

    [SerializeField] ScrollStopsView _scrollStopsView;

    [Inject] PlayersQueueModel _playersQueueModel;
    PlayersQueueViewModel _playersQueueViewModel;
    [SerializeField] PlayersQueueView _playersQueueView;
    [SerializeField] PlayersQueueRecordsView _playersQueueRecordsView;

    [Inject] PropertyModel<Field> _fieldModel;
    FieldGridViewModel _fieldGridViewModel;
    [SerializeField] FieldGridView _fieldGridView;

    [Inject] PropertyModel<PlayersSorting> _playersSortingModel;
    PropertyViewModel<PlayersSorting> _playersSortingViewModel;

    [Inject] PropertyModel<MaxPoints> _maxPointsModel;
    PropertyViewModel<MaxPoints> _maxPointsViewModel;

    [Inject] CollectionModel<Record> _recordsSelectableCollectionModel;

    //PlayersRecordsModel _playersRecordsModel;
    //PlayersRecordsViewModel _playersRecordsViewModel;

    //winMessage
    //panels

    [SerializeField] Button _buttonSave;

    IPlayersSortingHeandler _sortingComand;


    private void Awake()
    {
        _playersQueueViewModel = new(_playersQueueModel);
        _playersQueueView.Init(_playersQueueViewModel);
        _playersQueueRecordsView.Init(_playersQueueViewModel);

        _fieldGridViewModel = new(_fieldModel);
        _fieldGridView.Init(_fieldGridViewModel);
        _fieldGridViewModel.onSelected += TileSelect;

        _playersSortingViewModel = new(_playersSortingModel);


        _maxPointsViewModel = new(_maxPointsModel);



        _sortingComand = _playersSortingViewModel.property.Value.SortingComand;


    }

    private void Start()
    {
        _buttonSave.onClick.AddListener(SaveRecord);
        _buttonSave.interactable = false;

        StartRound();

    }

    void StartRound()
    {

        foreach (var line in _fieldGridViewModel.Tiles)
            foreach (var tile in line)
                tile.Model = null;

        //Debug.Log(_fieldGridViewModel.Tiles[0][0].Mark);

        StartTurn();
    
    }

    void StartTurn()
    {
        _fieldGridViewModel.interactible = false;

        _playersQueueViewModel.FirstPlayer.Behaviour.Value.Action(_fieldGridViewModel, _playersQueueViewModel);
    
    }

    void TileSelect(PlayerViewModel tile)
    {
        if (tile.Model != null)
            return;

        tile.Model = _playersQueueViewModel.playersViewModels.Value[0].Model;

        _fieldGridViewModel.interactible = false;

        EndTurn();

    }

    void EndTurn()
    {
        _fieldGridViewModel.interactible = false;

        PlayerViewModel winner = _fieldGridViewModel.Winner;

        if (winner != null)
        {
            StartCoroutine(EndingRound(winner));
            return;
        }
        if (!_fieldGridViewModel.Avaliable)
        {
            StartCoroutine(EndingRound(null));
            return;
        }

        _playersQueueViewModel.InputMovePlayer(0, _playersQueueViewModel.playersViewModels.Value.Count - 1);

        StartTurn();

    }


    float animationTime = 1;
    IEnumerator EndingRound(PlayerViewModel winner)
    {
        yield return new WaitForSeconds(animationTime);
        EndRound(winner);
    }

    void EndRound(PlayerViewModel winner)
    {
        _buttonSave.interactable = true;

        if (winner != null)
        {
            winner.InputAddPoint();
            //Debug.Log(winner.Name);
        }

        if (winner == null)
        {
            //Debug.Log("team");
        }

        _sortingComand.Sort(_playersQueueViewModel.playersViewModels);

        if (_playersQueueViewModel.playersViewModels.Value.Any( player => player.Points.Value >= _maxPointsViewModel.property.Value.maxPoints ))
        {
            _scrollStopsView.SetPositionByIndex(1);
            return;
        }
        
        StartRound();

    }

    void SaveRecord()
    {
        List<PlayerModel> playersModels = new();
        foreach(PlayerModel player in _playersQueueModel.playerModels.Value)
            playersModels.Add(new(player.Hue.Value, player.Mark.Value, player.Name.Value, player.Behaviour.Value, player.Points.Value));
        
        Record record = new Record(playersModels, _fieldModel.property.Value, _playersSortingModel.property.Value, _maxPointsModel.property.Value);
        _recordsSelectableCollectionModel.collection.Value.Add(new(record));
        _buttonSave.interactable = false;

        Debug.Log("RecordSaved");

    }


}
