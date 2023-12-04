using System.Collections.ObjectModel;
using UnityEngine;

public class PlayersQueueRecordsView : MonoBehaviour
{
    PlayersQueueViewModel _viewModel;

    [SerializeField] PlayerView _playerViewPrefab;
    [SerializeField] RTCollection _rtCollection;
    ObservableCollection<PlayerView> _playerViews = new();
    

    public void Init(PlayersQueueViewModel viewModel)
    {
        _viewModel = viewModel;

        ViewUpdate();

    }
    void ViewUpdate()
    {
        if (_viewModel != null)
        {
            OutputPlayersRecords(_viewModel.playersViewModels.Value);
        }
        else
        {
            OutputPlayersRecords(null);
        }

    }

    void OutputPlayersRecords(ObservableCollection<PlayerViewModel> playerViewModels)
    {
        foreach (var playerView in _playerViews)
            Remove(playerView);

        _playerViews = new();
        
        if (playerViewModels != null)
        {
            foreach (var playerViewModel in playerViewModels)
                Add(playerViewModel);
        }
        
        MoveByPoints();

    }

    void Add(PlayerViewModel playerViewModel)
    {
        playerViewModel.onAddPoint += MoveByPoints;
        PlayerView newPlayerView = Instantiate(_playerViewPrefab);
        newPlayerView.Init(playerViewModel);
        _playerViews.Add(newPlayerView);
        _rtCollection.Add(newPlayerView);
    }
    void Move(int oldValue, int newValue)
    {
        _playerViews.Move(oldValue, newValue);
        _rtCollection.Move(oldValue, newValue);
    }
    void Remove(PlayerView playerView)
    {
        playerView.ViewModel.onAddPoint -= MoveByPoints;
        _rtCollection.Remove(playerView);
        //_playerViews.Remove(playerView);
        Destroy(playerView.gameObject);
    }

    void MoveByPoints()
    {
        if (_playerViews.Count < 2) return;

        for (int i = 1; i < _playerViews.Count; i++)
            for (int j = 0; j < _playerViews.Count - 1; j++)
            {
                if (_playerViews[j].ViewModel.Points.Value < _playerViews[j + 1].ViewModel.Points.Value)
                    Move(j, j+1);
            }

    }


}
