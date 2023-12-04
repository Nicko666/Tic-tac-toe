using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewTheme", menuName = "ScriptableObjects/Theme")]
public class Theme : ScriptableObject
{
    public Color backgroundColor;
    public Color playerFontColor;
    public Color uiFontColor;

}
