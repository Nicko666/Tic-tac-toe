using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RTCollectionDrag : RTCollectionClick
{
    public UnityEvent<int, int> onMoveRequest;


    public override void Add(MonoBehaviour item)
    {
        RTDrag rtDrag = item.GetOrAddComponent<RTDrag>();
        _RTs.Add(rtDrag);
        
        rtDrag.onEnterRT += MoveRequest;
        rtDrag.onClickRT += RTClick;

        UpdateItems();

    }

    public override void Remove(MonoBehaviour item)
    {
        RTDrag rtDrag = item.GetComponent<RTDrag>();
        _RTs.Remove(rtDrag);
        
        rtDrag.onEnterRT -= MoveRequest;
        rtDrag.onClickRT -= RTClick;

        UpdateItems();
    
    }

    void MoveRequest(RT oldItem, RT newItem)
    {
        int oldIndex = _RTs.IndexOf(oldItem);
        int newIndex = _RTs.IndexOf(newItem);

        onMoveRequest?.Invoke(oldIndex, newIndex);

    }


}
