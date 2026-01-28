using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
internal class LineTilePresenter : RuleTile<LinesPresenter>
{
    private LinePresenter _linePresenter;

    internal void Output(bool show, Color color)
    {
        //_linePresenter.Output(show, color);
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject instantiatedGameObject)
    {
        if (!instantiatedGameObject)
            if (instantiatedGameObject.TryGetComponent(out LinePresenter linePresenter))
                _linePresenter = linePresenter;

        Debug.Log("LineTilePresenter.StartUp");

        return base.StartUp(position, tilemap, instantiatedGameObject);
    }
}
