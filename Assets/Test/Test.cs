using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private Image _image;
    [SerializeField] private float _duration = 1;
    private readonly List<IEnumerator> _routines = new();
    private Coroutine _coroutine = null;

    private void Start()
    {
        foreach (var color in _colors)
            _routines.Add(OutputBoardRoutine(color, _duration));
        
        _coroutine ??= StartCoroutine(OutputBoardCoroutine());
    }


    private IEnumerator OutputBoardCoroutine()
    {
        while (_routines.Count > 0)
        {
            yield return _routines[0];
            _routines.RemoveAt(0);
        }
        _coroutine = null;
    }

    private IEnumerator OutputBoardRoutine(Color color, float duration)
    {
        Debug.Log($"is Started");
        yield return new WaitForSeconds(duration);
        Debug.Log($"is Finished");
        _image.color = color;
    }
}
