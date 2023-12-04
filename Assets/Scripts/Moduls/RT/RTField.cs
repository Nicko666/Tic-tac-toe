using System;
using Unity.VisualScripting;
using UnityEngine;

public class RTField : MonoBehaviour
{
    [SerializeField] RectTransform _contentField;

    RTClick[,] _items;

    Vector2 _direction = new Vector2(1, -1);


    //public delegate void onRTClickDelegate(int line, int column);
    //public onRTClickDelegate onRTClick;
    public Action<int, int> onRTClick;


    public void CreateRTField(MonoBehaviour[][] rtItems)
    {
        if (_contentField.pivot != Vector2.up)
        {
            //change Pivot
            Vector2 oldValue = _contentField.pivot;
            _contentField.pivot = Vector2.up;
            _contentField.localPosition = new Vector2(_contentField.localPosition.x, _contentField.localPosition.y) + ((Vector2.up - oldValue) * _contentField.sizeDelta);
        }

        _items = new RTClick[rtItems.GetLength(0), rtItems[0].GetLength(0)];

        //Vector2 rtSiseDelta = new Vector2(_contentField.sizeDelta.x / rtItems[0].GetLength(0), _contentField.sizeDelta.y / rtItems.GetLength(0)).Abs();
        Vector2 rtSiseDelta = new Vector2( MathF.Abs(_contentField.sizeDelta.x / rtItems[0].GetLength(0)), MathF.Abs(_contentField.sizeDelta.y / rtItems.GetLength(0)) );
        Vector2 rtStartPosition = rtSiseDelta * _direction / 2;

        for (int l = 0; l < _items.GetLength(0); l++)
        {
            for (int c = 0; c < _items.GetLength(1); c++)
            {
                var newItem = rtItems[l][c].GetOrAddComponent<RTClick>();
                _items[l, c] = newItem;

                newItem.onClickRT += ClickCell;
                newItem.transform.SetParent(_contentField, false);
                newItem.SizeDelta = rtSiseDelta;
                newItem.name = $"Cell({l},{c})";
                newItem.SizeDelta = rtSiseDelta;
                newItem.LocalPosition = rtStartPosition + (rtSiseDelta * (_direction * new Vector2(c, l)));
            }
        }

    }

    void ClickCell(RTClick rtClick)
    {
        for (int l = 0; l < _items.GetLength(0); l++)
        {
            for (int c = 0; c < _items.GetLength(1); c++)
            {
                if (_items[l, c] == rtClick)
                {
                    onRTClick?.Invoke(l, c);
                    return;
                }
            }
        }

    }


}