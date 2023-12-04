using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class MenuAppSettingsMain : MonoBehaviour
{
    [Inject] DatabaseModel<Locals> _localsDatabaseModel;
    SelectablesCollectionViewModel<Locals> _localsSelectableCollectionViewModel;
    [SerializeField] SelectableCollectionView<Locals> _localsSelectableCollectionView;

    [Inject] DatabaseModel<Theme> _themeDatabaseModel;
    SelectablesCollectionViewModel<Theme> _themesSelectableCollectionViewModel;
    [SerializeField] SelectableCollectionView<Theme> _themesSelectableCollectionView;

    [Inject] AppSettingsModel _appSettingsModel;
    AppSettingsViewModel _appSettingsViewModel;
    [SerializeField] AppSettingsView _appSettingsView;

    private void Awake()
    {
        _localsSelectableCollectionViewModel = new(new(_localsDatabaseModel.Items));
        _localsSelectableCollectionView.Init(_localsSelectableCollectionViewModel);
        _localsSelectableCollectionViewModel.onItemSelected += ChangeLocals;

        _themesSelectableCollectionViewModel = new(new(_themeDatabaseModel.Items));
        _themesSelectableCollectionView.Init(_themesSelectableCollectionViewModel);
        _themesSelectableCollectionViewModel.onItemSelected += ChangeTheme;

        _appSettingsViewModel = new(_appSettingsModel);
        _appSettingsView.Init(_appSettingsViewModel);
        
    }

    private void Start()
    {
        _themesSelectableCollectionViewModel.OutputItemSelect(_appSettingsModel.theme.Value);
        _localsSelectableCollectionViewModel.OutputItemSelect(_appSettingsModel.locals.Value);

        StartCoroutine(StartDelay());

    }

    IEnumerator StartDelay()
    {
        yield return new WaitForNextFrameUnit();
        _appSettingsViewModel.OutputLocal(_appSettingsModel.locals.Value);

    }

    void ChangeLocals(Locals value)
    {
        _appSettingsViewModel.InputLocalisation(value);
        //_appSettingsViewModel.IntputTheme(value);
    }
    void ChangeTheme(Theme value)
    {
        _appSettingsViewModel.IntputTheme(value);
    }
    void ChangeVolume(float value)
    {
        _appSettingsViewModel.InputVolume(value);
    }


}
