using System;
using UnityEngine.EventSystems;

public class RTClick : RT, IPointerClickHandler
{
    public Action<RTClick> onClickRT;


    public void OnPointerClick(PointerEventData eventData)
    {
        onClickRT?.Invoke(this);

    }


}
