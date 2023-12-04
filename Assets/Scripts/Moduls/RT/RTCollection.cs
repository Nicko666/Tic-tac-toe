using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class RTCollection : MonoBehaviour
{
    protected ObservableCollection<RT> _RTs = new();

    [SerializeField] ContentDirection _direction;
    [SerializeField] ContentSize _size;

    [SerializeField] RectTransform _contentField;
    [SerializeField] float _distance;
    [SerializeField] bool _streachContentField;

    public int Count => _RTs.Count;


    Vector2 Direction
    {
        get
        {
            switch (_direction)
            {
                case ContentDirection.Down:
                    return Vector2.down;
                case ContentDirection.Right:
                    return Vector2.right;
                default:
                    return Vector2.down;
            }
        }

    }
    Vector2 Pivot
    {
        get
        {
            switch (_direction)
            {
                case ContentDirection.Down:
                    return new Vector2(0.5f, 1);
                case ContentDirection.Right:
                    return new Vector2(0, 0.5f);
                default:
                    return new Vector2(0.5f, 1);
            }
        }

    }
    Vector2[] Positions
    {
        get
        {
            Vector2[] positions = new Vector2[_RTs.Count];

            if (_RTs.Count < 1) return positions;

            var direction = Direction;
            Vector2 directionStep = Vector2.zero;

            if (_size == ContentSize.FitIn)
            {
                directionStep = (_contentField.sizeDelta * direction) / _RTs.Count;
            }
            if (_size == ContentSize.Expanded)
            {
                directionStep = direction * _distance;
            }

            var startPosition = directionStep / 2;

            for (int i = 0; i < _RTs.Count; i++)
            {
                positions[i] = startPosition + (directionStep * i);
            }

            return positions;

        }

    }


    public virtual void Add(MonoBehaviour item)
    {
        var newItem = item.GetOrAddComponent<RT>();
        _RTs.Add(newItem);

        UpdateItems();
        
    }

    public virtual void Move(int oldIndex, int newIndex)
    {
        _RTs.Move(oldIndex, newIndex);

        UpdateItems();
    }

    public virtual void Remove(MonoBehaviour item)
    {
        _RTs.Remove(item.GetComponent<RT>());
        
        UpdateItems();

    }

    protected void UpdateItems()
    {
        SetCanvasPivot();
        SetSizes();
        SetPositions();
    }

    void SetCanvasPivot()
    {
        var newPivot = Pivot;

        if (_contentField.pivot != newPivot)
        {
            ChangePivot(_contentField, newPivot);
        }
        
        void ChangePivot(RectTransform rectTransform, Vector2 value)
        {
            Vector2 oldValue = rectTransform.pivot;
            rectTransform.pivot = value;
            rectTransform.localPosition = new Vector2(rectTransform.localPosition.x, rectTransform.localPosition.y) + ((value - oldValue) * rectTransform.sizeDelta);
        }

    }

    void SetSizes()
    {
        Vector2 direction = Direction;
        Vector2 oppositDirection = new(direction.y, direction.x);
        int count = _RTs.Count;

        if (_size == ContentSize.FitIn)
        {
            Vector2 objSizeDelta = ( (_contentField.sizeDelta * (direction / count)) + (_contentField.sizeDelta * oppositDirection) );
            objSizeDelta = new Vector2 ( Mathf.Abs(objSizeDelta.x), Mathf.Abs(objSizeDelta.y));
            foreach (var item in _RTs)
                item.SizeDelta = objSizeDelta;
        }
        if (_size == ContentSize.Expanded)
        {
            if (!_streachContentField) return;
            Vector2 temp = (direction * count * _distance) + (_contentField.sizeDelta * oppositDirection);
            _contentField.sizeDelta = new Vector2(Mathf.Abs(temp.x), Mathf.Abs(temp.y));
        }

    }

    void SetPositions()
    {
        Vector2[] positions = Positions;

        for (int i = 0; i < _RTs.Count; i++)
        {
            _RTs[i].SetParent = _contentField;
            _RTs[i].LocalScale = Vector2.one;
            _RTs[i].LocalPosition = Positions[i];
        }

    }

    
}



enum ContentDirection
{
    Down,
    Right
}
enum ContentSize
{
    FitIn,
    Expanded
}