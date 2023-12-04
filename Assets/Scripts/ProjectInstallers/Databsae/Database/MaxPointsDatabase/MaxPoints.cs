using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMaxPoints", menuName = "ScriptableObjects/MaxPoints")]
[Serializable]
public class MaxPoints : ScriptableObject, ISprite
{
    [SerializeField] Sprite _sprite;
    [SerializeField] int _maxPoints;
    
    public int maxPoints => _maxPoints;


    public Sprite GetSprite() => _sprite;


}
