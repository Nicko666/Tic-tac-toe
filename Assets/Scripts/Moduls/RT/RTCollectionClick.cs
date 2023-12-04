using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RTCollectionClick : RTCollection
{
    public UnityEvent<int> onClick;


    public override void Add(MonoBehaviour item)
    {   
        RTClick rtClick = item.GetOrAddComponent<RTClick>();
        _RTs.Add(rtClick);
        rtClick.onClickRT += RTClick;

        UpdateItems();

    }

    public override void Remove(MonoBehaviour item)
    {
        RTClick rtClick = item.GetComponent<RTClick>();
        _RTs.Remove(rtClick);
        rtClick.onClickRT -= RTClick;

        UpdateItems();

    }

    protected void RTClick(RTClick rtClick)
    {
        int index = _RTs.IndexOf(rtClick);

        onClick?.Invoke(index);

    }


}