using System.Linq;
using UnityEngine;
using Zenject;

public class MenuPlayersSettingsMain : MonoBehaviour
{
    [SerializeField] ScrollStopsView scrollStopsView;

    [Inject] RandomPlayerGenerator _randomPlayerGenerator;

    [Inject] PlayersQueueModel _playersQueueModel;
    PlayersQueueViewModel _playersQueueViewModel;
    [SerializeField] PlayersQueueView _playersQueueView;


    PlayerViewModel _selectedPlayerViewModel;
    [SerializeField] PlayerView _selectedPlayerView;

    PlayerNameModel _playerNameModel = new();
    PlayerNameViewModel _playerNameViewModel;
    [SerializeField] PlayerNameView _playerNameView;

    [Inject] DatabaseModel<PlayerMark> _playerMarkDatabase;
    SelectablesCollectionViewModel<PlayerMark> _playerMarksViewModels;
    [SerializeField] SelectableCollectionLayoutView<PlayerMark> _playerMarksView;

    [Inject] DatabaseModel<PlayerBehaviour> _playerBehavioursDatabase;
    SelectablesCollectionViewModel<PlayerBehaviour> _playerBehavioursViewModels;
    [SerializeField] SelectableCollectionLayoutView<PlayerBehaviour> _playerBehavioursView;

    PlayerHueModel _playerHueModel = new();
    PlayerHueViewModel _playerHueViewModel;
    [SerializeField] PlayerHueView _playerHueView;


    private void Awake()
    {
        _playersQueueViewModel = new(_playersQueueModel);
        _playersQueueView.Init(_playersQueueViewModel);
        _playersQueueViewModel.onSelect += SelectPlayerAndScroll;
        _playersQueueViewModel.onAddRequest += AddRandomPlayer;
        _playersQueueViewModel.onRemoved += SelectFirstPlayer;


        _playerNameViewModel = new(_playerNameModel);
        _playerNameView.Init(_playerNameViewModel);
        _playerNameViewModel.onNameChanged += ChangePlayerName;

        _playerMarksViewModels = new( new (_playerMarkDatabase.Items) );
        _playerMarksView.Init(_playerMarksViewModels);
        _playerMarksViewModels.onItemSelected += ChangePlayerMark;

        _playerBehavioursViewModels = new( new(_playerBehavioursDatabase.Items) );
        _playerBehavioursView.Init(_playerBehavioursViewModels);
        _playerBehavioursViewModels.onItemSelected += ChangePlayerBehaviour;
        //_playerMarksViewModels.OutputItemSelect();

        _playerHueViewModel = new( _playerHueModel);
        _playerHueView.Init(_playerHueViewModel);
        _playerHueViewModel.onHueChanged += ChangePlayerHue;

    }

    private void Start()
    {
        SelectPlayer(_playersQueueViewModel.playersViewModels[0]);
        //scrollStopsView.SetPositionByIndex(2);

    }

    void SelectPlayer(PlayerViewModel playerViewModel)
    {
        _selectedPlayerViewModel = playerViewModel;
        _selectedPlayerView.Init(_selectedPlayerViewModel);

        _playerNameViewModel.OutputName(playerViewModel.Name.Value);
        _playerMarksViewModels.OutputItemSelect(playerViewModel.Mark.Value);
        _playerHueViewModel.OutputHue(playerViewModel.Hue.Value);
        _playerBehavioursViewModels.OutputItemSelect(playerViewModel.Behaviour.Value);

    }
    void SelectFirstPlayer()
    {
        SelectPlayer(_playersQueueViewModel.playersViewModels.Value[0]);
    }
    void SelectPlayerAndScroll(PlayerViewModel playerViewModel)
    {
        SelectPlayer(playerViewModel);

        scrollStopsView.SetPositionByIndex(3);

    }
    void ChangePlayerName(string value)
    {
        if (_selectedPlayerViewModel != null)
            _selectedPlayerViewModel.InputName(value);
    }
    void ChangePlayerMark(PlayerMark obj)
    {
        if (_selectedPlayerViewModel != null)
            _selectedPlayerViewModel.InputMark(obj);
    }
    void ChangePlayerBehaviour(PlayerBehaviour obj)
    {
        if (_selectedPlayerViewModel != null)
            _selectedPlayerViewModel.InputBehaviour(obj);
    }
    void ChangePlayerHue(float value)
    {
        if (_selectedPlayerViewModel != null)
            _selectedPlayerViewModel.InputHue(value);
    }
    void AddRandomPlayer()
    {
        _playersQueueViewModel.InputAddPlayer(_randomPlayerGenerator.PlayerModel);
    }


}
