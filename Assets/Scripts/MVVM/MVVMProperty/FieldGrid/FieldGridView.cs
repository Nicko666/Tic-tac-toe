using UnityEngine;

public class FieldGridView : MonoBehaviour
{
    FieldGridViewModel _fieldViewModel;

    PlayerView[][] _playerViews;

    [SerializeField] RTField _rtField;
    [SerializeField] PlayerView _playerViewPrefab;


    public void Init(FieldGridViewModel fieldViewModel)
    {
        if (_fieldViewModel != null)
            ViewUnsubscribe();

        _fieldViewModel = fieldViewModel;

        if (_fieldViewModel != null)
            ViewSubscribe();

        ViewUpdate();

    }

    void ViewSubscribe()
    {
        _fieldViewModel.tiles.onValueChanged += OutputField;

    }
    void ViewUnsubscribe()
    {
        _fieldViewModel.tiles.onValueChanged -= OutputField;

    }
    void ViewUpdate()
    {
        if (_fieldViewModel != null)
            OutputField(_fieldViewModel.tiles.Value);
    }

    public void OutputField(PlayerViewModel[][] field)
    {
        if (_playerViews != null)
        {
            for (int line = 0; line < _playerViews.Length; line++)
            {
                for (int column = 0; column < _playerViews[line].Length; column++)
                {
                    Destroy(_playerViews[line][column].gameObject);
                }
            }

            _playerViews = new PlayerView[0][];
        }

        if (field != null)
        {
            _playerViews = new PlayerView[field.Length][];

            for (int line = 0; line < field.Length; line++)
            {
                _playerViews[line] = new PlayerView[field[line].Length];

                for (int column = 0; column < _playerViews[line].Length; column++)
                {
                    _playerViews[line][column] = Instantiate(_playerViewPrefab, transform);
                    _playerViews[line][column].Init(field[line][column]);
                }
            }
            
            _rtField.CreateRTField(_playerViews);
        }

    
    }


}