using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LinesPresenter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tile;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _maskTransform;

    private readonly static int AnimatorBool = Animator.StringToHash("IsShown");

    internal void Output(in LineModel[] lines)
    {
        _animator.SetBool(AnimatorBool, lines.Length > 0);
        if (lines.Length == 0) return;
        
        PlayerModel player = lines[0].winner;
        _tilemap.color = Color.HSVToRGB(player.hue, player.saturation, 1);

        _tilemap.ClearAllTiles();
        Array.ForEach(lines, line =>
            Array.ForEach(line.tilesIndex, tileIndex =>
            {
                Vector3Int position = new(tileIndex.x, tileIndex.y);

                if (_tilemap.GetTile(position) == null)
                    _tilemap.SetTile(position, _tile);
            })
        );

        _maskTransform.localScale = Vector3.one * (_tilemap.size.x > _tilemap.size.y ? _tilemap.size.x : _tilemap.size.y);
    }
}
