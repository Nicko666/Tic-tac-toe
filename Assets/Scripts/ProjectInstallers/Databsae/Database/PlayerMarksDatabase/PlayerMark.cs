using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerMark", menuName = "ScriptableObjects/PlayerMark")]
public class PlayerMark : ScriptableObject, ISprite
{
    [SerializeField] Sprite _sprite;
 

    public Sprite GetSprite() => _sprite;


}
