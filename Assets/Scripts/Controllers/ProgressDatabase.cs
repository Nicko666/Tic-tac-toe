using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Scriptable Objects/Database")]
public class ProgressDatabase : ScriptableObject
{
    [field: SerializeField] internal int MinPlayersCount { get; private set; }
    [field: SerializeField] internal BoardModel[] Boards { get; private set; }
    [field: SerializeField] internal LevelModel[] Levels { get; private set; }
    [field: SerializeField] internal LogicModel[] Logics { get; private set; }
    [field: SerializeField] internal MarkModel[] Marks { get; private set; }
}