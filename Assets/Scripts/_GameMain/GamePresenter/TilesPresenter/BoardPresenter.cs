using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPresenter : MonoBehaviour
{
    [SerializeField] private TilesPresenter _tilesPresenter;
    [SerializeField] private LinesPresenter _horizontalLines, _verticalLines, _upLines, _downLines;
    [SerializeField] private Transform[] _centerObjects;
    [SerializeField] private Animator _animator;
    private GameBoardModel _board;
    private float _duration = 0;
    private readonly List<IEnumerator> _routines = new();
    private Coroutine _coroutine = null;
    private static int WinnerBool => Animator.StringToHash("Selectable");
    bool _isInteractable = true;

    internal Action<GamePlayerModel[]> onPlayerQueueChanged;
    internal event Action<Vector2Int> onInputTile;
    internal event Action onInputClearBoard;

    internal void OutputBoard(GameBoardModel board)
    {
        _board = board;
        _routines.Add(OutputBoardRoutine(_board, _duration));

        _coroutine ??= StartCoroutine(OutputCoroutine());

        LineModel[] horizontalLines = Array.FindAll(board.Lines, i => i.winner != null && i.direction == LineModel.DirectionType.Horizontal);
        _horizontalLines.Output(horizontalLines);
        LineModel[] verticalLines = Array.FindAll(board.Lines, i => i.winner != null && i.direction == LineModel.DirectionType.Vertical);
        _verticalLines.Output(verticalLines);
        LineModel[] upLines = Array.FindAll(board.Lines, i => i.winner != null && i.direction == LineModel.DirectionType.Up);
        _upLines.Output(upLines);
        LineModel[] downLines = Array.FindAll(board.Lines, i => i.winner != null && i.direction == LineModel.DirectionType.Down);
        _downLines.Output(downLines);
    }

    internal void OutputDelay(float duration) =>
        _duration = duration;

    private void Awake()
    {
        _tilesPresenter.onCenterChanged += OutputFieldCenter;
        _tilesPresenter.onInputTile += InputTile;
    }
    private void OnDestroy()
    {
        _tilesPresenter.onCenterChanged -= OutputFieldCenter;
        _tilesPresenter.onInputTile -= InputTile;
    }

    private void InputTile(Vector2Int tileIndex)
    {
        if (_isInteractable)
        {
            if (_board.IsInteractable)
                onInputTile.Invoke(tileIndex);
            else
                onInputClearBoard.Invoke();
        }
    }

    private void OutputFieldCenter(Vector2 position) =>
        Array.ForEach(_centerObjects, i => i.position = new Vector3(position.x, position.y, i.position.z));

    private IEnumerator OutputCoroutine()
    {
        while (_routines.Count > 0)
        {
            yield return _routines[0];
            _routines.RemoveAt(0);
        }
        _coroutine = null;
    }

    private IEnumerator OutputBoardRoutine(GameBoardModel board, float duration)
    {
        _isInteractable = false;

        yield return new WaitForSeconds(duration);
        _tilesPresenter.OutputTiles(board.Tiles, board.Lines);

        _animator.SetBool(WinnerBool, board.Winner == null);

        _isInteractable = true;
    }
}
