using System;
using UnityEngine;

[Serializable]
public class QueueModel
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public QueueType Queue { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [Serializable]
    public enum QueueType
    {
        WinnerFirst = 0,
        WinnerLast = 1
    }
}