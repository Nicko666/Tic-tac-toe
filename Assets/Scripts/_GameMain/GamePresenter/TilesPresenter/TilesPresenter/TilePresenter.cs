using UnityEngine;

public class TilePresenter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private Animator _animator;

    private static readonly int IsPlayerBool = Animator.StringToHash("IsPlayer");
    private const float ColorValue = 1.0f;

    internal void OutputTile(TileModel tileModel)
    {
        _animator.SetBool(IsPlayerBool, tileModel.player != null);

        
        if (tileModel.player != null)
        {
            Color color = Color.HSVToRGB(tileModel.player.hue, tileModel.player.saturation, ColorValue);

            _iconRenderer.sprite = tileModel.player.mark.Icon;
            _iconRenderer.color = color;
        }
    }
}
