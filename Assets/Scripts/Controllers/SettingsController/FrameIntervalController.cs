using System;
using UnityEngine;

public class FrameIntervalController
{
    private FrameIntervalModel[] _database = new FrameIntervalModel[0];
    public FrameIntervalModel _current;

    internal FrameIntervalModel Get() => _current;

    internal void SetDatabase(in FrameIntervalModel[] frameIntervals)
    {
        _database = frameIntervals;

        if (Array.Exists(_database, i => i.Interval == _current.Interval))
            _current = Array.Find(_database, i => i.Interval == _current.Interval);
        else
            _current = _database[0];

        Application.targetFrameRate = _current.FramesCount;
    }

    internal void Set(FrameIntervalModel frameInterval)
    {
        if (Array.Exists(_database, i => i.Interval == frameInterval.Interval))
            _current = Array.Find(_database, i => i.Interval == frameInterval.Interval);
        else
            _current = _database[0];

        Application.targetFrameRate = _current.FramesCount;
    }

    internal void Load(int frameInterval)
    {
        if (Array.Exists(_database, i => i.Interval == frameInterval))
            _current = Array.Find(_database, i => i.Interval == frameInterval);
        else
            _current = _database[0];

        Application.targetFrameRate = _current.FramesCount;
    }
    internal void Save(ref int frameInterval)
    {
        frameInterval = _current.Interval;
    }
}