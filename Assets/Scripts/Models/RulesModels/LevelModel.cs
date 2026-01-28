using System;
using UnityEngine;

[Serializable]
public class LevelModel
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public int Points { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
