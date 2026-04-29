using System;
using UnityEngine;

[Serializable]
public class LogicModel
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public LogicType Logic { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    public enum LogicType
    {
        Player = 0,
        ComputerLight = 1,
        ComputerHard = 2,
    }
}
