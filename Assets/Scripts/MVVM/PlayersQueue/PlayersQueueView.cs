using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayersQueueView : MonoBehaviour
{
    protected PlayersQueueViewModel _viewModel;

    [SerializeField] Button _addPlayerButton;
    [SerializeField] Button _removePlayerButton;
    [SerializeField] protected RTCollection _rtCollection;
    [SerializeField] PlayerView _viewPrefab;

    protected ObservableCollection<PlayerView> _views = new();


    public void Init(PlayersQueueViewModel viewModel)
    {
        if (_viewModel != null)
            Unsubscribe();

        _viewModel = viewModel;

        if (_viewModel != null)
            Subscribe();

        UpdateView();

    }

    void Subscribe()
    {
        _viewModel.playersViewModels.onValueChanged += OnPlayersValueChanged;
        _viewModel.playersViewModels.onAdd += OnPlayersAdd;
        _viewModel.playersViewModels.onMove += OnPlayersMove;
        _viewModel.playersViewModels.onRemove += OnPlayersRemove;
        _viewModel.playersMin.onValueChanged += OnPlayersMin;
        _viewModel.playersMax.onValueChanged += OnPlayersMax;
    }
    void Unsubscribe()
    {
        _viewModel.playersViewModels.onValueChanged -= OnPlayersValueChanged;
        _viewModel.playersViewModels.onAdd -= OnPlayersAdd;
        _viewModel.playersViewModels.onMove -= OnPlayersMove;
        _viewModel.playersViewModels.onRemove -= OnPlayersRemove;
        _viewModel.playersMin.onValueChanged -= OnPlayersMin;
        _viewModel.playersMax.onValueChanged -= OnPlayersMax;
    }
    void UpdateView()
    {
        OnPlayersValueChanged(_viewModel.playersViewModels.Value);
        OnPlayersMin(_viewModel.playersMin.Value);
        OnPlayersMax(_viewModel.playersMax.Value);
    }

    void OnPlayersValueChanged(ObservableCollection<PlayerViewModel> viewModels)
    {
        for (int i = 0; i < _views.Count; i++)
            Destroy(_views[i].gameObject);

        _views = new();

        for (int i = 0; i < viewModels.Count; i++)
            OnPlayersAdd(viewModels[i]);
    
    }
    void OnPlayersAdd(PlayerViewModel viewModel)
    {
        var newView = Instantiate(_viewPrefab);
        
        _views.Add(newView);
        _rtCollection.Add(newView);
        
        newView.Init(viewModel);

    }
    void OnPlayersMove(int oldIndex, int newIndex)
    {
        _views.Move(oldIndex, newIndex);
        _rtCollection.Move(oldIndex, newIndex);
    }
    void OnPlayersRemove(PlayerViewModel viewModel)
    {
        while ( _views.Any(view => view.ViewModel == viewModel))
        {
            PlayerView playerView = _views.First(view => view.ViewModel == viewModel);

            _views.Remove(playerView);
            _rtCollection.Remove(playerView);

            Destroy(playerView.gameObject);
        }

    }
    void OnPlayersMin(bool value)
    {
        if (_removePlayerButton != null)
            _removePlayerButton.interactable = value;

    }
    void OnPlayersMax(bool value)
    {
        if (_addPlayerButton != null)
            _addPlayerButton.interactable = value;

    }

    public void InputAdd()
    {
        _viewModel.RequestAddPlayer();
    }
    public void InputMove(int oldValue, int newValue)
    {
        _viewModel.RequestMovePlayer(oldValue, newValue);
    }
    public void InputRemove()
    {
        _viewModel.RequestRemovePlayer();
    }


}
