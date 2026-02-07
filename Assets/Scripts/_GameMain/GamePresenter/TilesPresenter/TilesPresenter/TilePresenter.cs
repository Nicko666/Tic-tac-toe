using System;
using UnityEngine;

public class TilePresenter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private Animator _animator;
    private static readonly int IsPlayerBool = Animator.StringToHash("IsPlayer");
    private const float ColorValue = 1.0f;

    private PlayerModel _player;

    internal Action<SoundModel> onInputSound;

    internal void OutputTile(TileModel tileModel)
    {
        if (_player != tileModel.player && tileModel.player != null)
            onInputSound.Invoke(SoundModel.Move);

        _player = tileModel.player;

        _animator.SetBool(IsPlayerBool, _player != null);
        
        if (_player != null)
        {
            Color color = Color.HSVToRGB(tileModel.player.hue, tileModel.player.saturation, ColorValue);

            _iconRenderer.sprite = tileModel.player.mark.Icon;
            _iconRenderer.color = color;
        }
    }
}
