using UnityEngine;
using Zenject;

public class MenuGameSettingsMain : MonoBehaviour
{
    [Inject] PropertyModel<Field> _fieldModel;
    PropertyViewModel<Field> _fieldViewModel;
    [SerializeField] FieldImageView _fieldView;

    [Inject] void SetFields(DatabaseModel<Field> fieldDatabase)
    {
        _fieldsDatabaseViewModel = new(new(fieldDatabase.Items));
        _fieldsDatabaseView.Init(_fieldsDatabaseViewModel);
        _fieldsDatabaseViewModel.onItemSelected += ChangeField;
    }
    SelectablesCollectionViewModel<Field> _fieldsDatabaseViewModel;
    [SerializeField] SelectableCollectionLayoutView<Field> _fieldsDatabaseView;


    [Inject] PropertyModel<PlayersSorting> _playersSortingModel;
    PropertyViewModel<PlayersSorting> _playersSortingViewModel;
    [SerializeField] PropertyView<PlayersSorting> _playersSortingView;

    [Inject] void SetSortings(DatabaseModel<PlayersSorting> _playersSortingDatabaseModel)
    {
        _playersSortingDatabaseViewModel = new(new(_playersSortingDatabaseModel.Items));
        _playersSortingDatabaseView.Init(_playersSortingDatabaseViewModel);
        _playersSortingDatabaseViewModel.onItemSelected += ChangePlayerSorting;
    }
    SelectablesCollectionViewModel<PlayersSorting> _playersSortingDatabaseViewModel;
    [SerializeField] SelectableCollectionLayoutView<PlayersSorting> _playersSortingDatabaseView;


    [Inject] PropertyModel<MaxPoints> _maxPointsModel;
    PropertyViewModel<MaxPoints> _maxPointsViewModel;
    [SerializeField] PropertyView<MaxPoints> _maxPointsView;

    [Inject] void SetMaxPoints(DatabaseModel<MaxPoints> _maxPointsDatabaseModel)
    {
        _maxPointsDatabaseViewModel = new(new(_maxPointsDatabaseModel.Items));
        _maxPointsDatabaseView.Init(_maxPointsDatabaseViewModel);
        _maxPointsDatabaseViewModel.onItemSelected += ChangeMaxPoints;
    }
    SelectablesCollectionViewModel<MaxPoints> _maxPointsDatabaseViewModel;
    [SerializeField] SelectableCollectionLayoutView<MaxPoints> _maxPointsDatabaseView;


    private void Awake()
    {
        _fieldViewModel = new(_fieldModel);
        _fieldView.Init(_fieldViewModel);

        _playersSortingViewModel = new(_playersSortingModel);
        _playersSortingView.Init(_playersSortingViewModel);

        _maxPointsViewModel = new(_maxPointsModel);
        _maxPointsView.Init(_maxPointsViewModel);

    }

    private void Start()
    {
        _fieldsDatabaseViewModel.OutputItemSelect(_fieldModel.property.Value);
        _playersSortingDatabaseViewModel.OutputItemSelect(_playersSortingModel.property.Value);
        _maxPointsDatabaseViewModel.OutputItemSelect(_maxPointsModel.property.Value);

    }

    void ChangeField(Field value)
    {
        _fieldModel.property.Value = value;
    }
    void ChangePlayerSorting(PlayersSorting value)
    {
        _playersSortingModel.property.Value = value;
    }
    void ChangeMaxPoints(MaxPoints value)
    {
        _maxPointsModel.property.Value = value;
    }


}
