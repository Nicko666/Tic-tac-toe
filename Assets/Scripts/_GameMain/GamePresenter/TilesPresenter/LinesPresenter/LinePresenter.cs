using System;
using UnityEngine;

internal class LinePresenter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    internal void Output(LineModel[] lines)
    {

    }

    internal void Output(PlayerModel winner)
    {
        if (winner == null)
            _renderer.enabled = false;
        else
        {
            _renderer.enabled = true;
            _renderer.color = Color.HSVToRGB(winner.hue, winner.saturation, 1f);
        }

    }
}
