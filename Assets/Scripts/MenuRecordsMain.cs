using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuRecordsMain : MonoBehaviour
{
    [Inject] CollectionModel<Record> _recordsModel;
    SelectablesCollectionViewModel<Record> _recordsViewModel;
    [SerializeField] SelectableCollectionView<Record> _recordsView;

    //SelectableViewModel<Record> _selectedRecordViewModel;

    Record _selectedRecord;

    [SerializeField] Button _buttonRemove;


    private void Awake()
    {
        _recordsViewModel = new(_recordsModel);
        _recordsView.Init(_recordsViewModel);
        _recordsViewModel.onItemSelected += SelectRecord;
    }

    private void Start()
    {
        _buttonRemove.onClick.AddListener(RemoveRecord);
        SelectRecord(null);
    }

    void SelectRecord(Record record)
    {
        if (_selectedRecord == record)
        {
            record = null;    
        }
        
        _selectedRecord = record;
        _recordsViewModel.OutputItemSelect(_selectedRecord);
        _buttonRemove.interactable = (_selectedRecord != null);

    }

    void RemoveRecord()
    {
        if( _selectedRecord != null)
        {
            _recordsViewModel.InputRemove(_selectedRecord);
        }

        SelectRecord(null);

    }


}
