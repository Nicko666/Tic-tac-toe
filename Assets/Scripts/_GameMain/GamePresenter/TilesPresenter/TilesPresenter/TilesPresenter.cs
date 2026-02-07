using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

internal class TilesPresenter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Tilemap _playersTilemap;
    [SerializeField] private TileBase _playerTile;
    [SerializeField] private Tilemap _borderTilemsp;
    [SerializeField] private TileBase _borderTile;

    internal Action<Vector2Int> onInputTile;
    internal Action<Vector2> onCenterChanged;
    internal Action<SoundModel> onInputSound;

    public void OnPointerClick(PointerEventData eventData) =>
        onInputTile.Invoke((Vector2Int)_playersTilemap.layoutGrid.WorldToCell(eventData.pointerCurrentRaycast.worldPosition));

    internal void OutputTiles(in TileModel[,] tiles, in LineModel[] lines)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (_playersTilemap.GetTile(new (x, y)) == null)
                {
                    _playersTilemap.SetTile(new(x, y), _playerTile);
                    _borderTilemsp.SetTile(new(x, y), _borderTile);
                    TilePresenter tilePresenter = _playersTilemap.GetInstantiatedObject(new(x, y)).GetComponent<TilePresenter>();
                    tilePresenter.onInputSound += InputSound;
                }

                GameObject tileObject = _playersTilemap.GetInstantiatedObject(new (x, y));
                if (!tileObject)
                    return;
                else if (tileObject.TryGetComponent(out TilePresenter tilePresenter))
                {
                    TileModel targetTile = tiles[x, y];
                    LineModel[] tileLines = Array.FindAll(lines, line => Array.Exists(line.tilesIndex, tile => tile.x == x && tile.y == y));

                    tilePresenter.OutputTile(tiles[x, y]);
                }
            }

        onCenterChanged.Invoke(_playersTilemap.localBounds.center);
    }

    private void InputSound(SoundModel sound) =>
        onInputSound.Invoke(sound);
}