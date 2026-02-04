using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

internal class TilesPresenter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tile;

    internal Action<Vector2Int> onInputTile;
    internal Action<Vector2> onCenterChanged;

    public void OnPointerClick(PointerEventData eventData) =>
        onInputTile.Invoke((Vector2Int)_tilemap.layoutGrid.WorldToCell(eventData.pointerCurrentRaycast.worldPosition));

    internal void OutputTiles(in TileModel[,] tiles, in LineModel[] lines)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (_tilemap.GetTile(new (x, y)) == null)
                    _tilemap.SetTile(new(x, y), _tile);

                GameObject tileObject = _tilemap.GetInstantiatedObject(new (x, y));
                if (!tileObject)
                    return;
                else if (tileObject.TryGetComponent(out TilePresenter tilePresenter))
                {
                    TileModel targetTile = tiles[x, y];
                    LineModel[] tileLines = Array.FindAll(lines, line => Array.Exists(line.tilesIndex, tile => tile.x == x && tile.y == y));

                    tilePresenter.OutputTile(tiles[x, y]);
                }
            }

        onCenterChanged.Invoke(_tilemap.localBounds.center);
    }
}