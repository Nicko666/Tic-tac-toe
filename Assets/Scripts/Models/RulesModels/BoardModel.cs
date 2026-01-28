using System;
using UnityEngine;

[Serializable]
public class BoardModel
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public int SquereTilesCount { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
