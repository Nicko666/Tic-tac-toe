using System;
using UnityEngine;

[Serializable]
public class MarkModel
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
