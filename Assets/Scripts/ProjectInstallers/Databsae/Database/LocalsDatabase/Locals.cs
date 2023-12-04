using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLocal", menuName = "ScriptableObjects/Local")]
[Serializable]
public class Locals : ScriptableObject
{
    [SerializeField] string _code;
    [SerializeField] string _description;

    public string Code => _code;
    public string Description => _description;


}
