using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Scriptable Objects/Database")]
public class ProgressDatabase : ScriptableObject
{
    [field: SerializeField] internal int MinPlayersCount { get; private set; }
    [field: SerializeField] internal float LoadingDelay { get; private set; }
    [field: SerializeField] internal BoardModel[] BoardsModel { get; private set; }
    [field: SerializeField] internal LevelModel[] LevelsModel { get; private set; }
    [field: SerializeField] internal LogicModel[] LogicsModel { get; private set; }
    [field: SerializeField] internal MarkModel[] Marks { get; private set; }
}