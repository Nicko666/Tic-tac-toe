using UnityEngine;

[CreateAssetMenu(fileName = "NewField", menuName = "ScriptableObjects/Field")]
public class Field : ScriptableObject, ISprite
{
    public int Size;
    
    public Sprite _sprite;


    public Sprite GetSprite() => _sprite;


}
