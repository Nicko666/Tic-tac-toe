using System;
using UnityEngine;

public class FrameIntervalController
{
    private FrameIntervalModel[] _frameIntervals = new FrameIntervalModel[0];
    public FrameIntervalModel _frameInterval;

    internal FrameIntervalModel Get() => _frameInterval;

    internal void SetDatabase(in FrameIntervalModel[] frameIntervals)
    {
        _frameIntervals = frameIntervals;

        if (Array.Exists(_frameIntervals, i => i.Interval == _frameInterval.Interval))
            _frameInterval = Array.Find(_frameIntervals, i => i.Interval == _frameInterval.Interval);
        else
            _frameInterval = _frameIntervals[0];

        Application.targetFrameRate = _frameInterval.FramesCount;
    }

    internal void Set(FrameIntervalModel frameInterval)
    {
        if (Array.Exists(_frameIntervals, i => i.Interval == frameInterval.Interval))
            _frameInterval = Array.Find(_frameIntervals, i => i.Interval == frameInterval.Interval);
        else
            _frameInterval = _frameIntervals[0];

        Application.targetFrameRate = _frameInterval.FramesCount;
    }

    internal void Load(int frameInterval)
    {
        if (Array.Exists(_frameIntervals, i => i.Interval == frameInterval))
            _frameInterval = Array.Find(_frameIntervals, i => i.Interval == frameInterval);
        else
            _frameInterval = _frameIntervals[0];

        Application.targetFrameRate = _frameInterval.FramesCount;
    }
    internal void Save(ref int frameInterval)
    {
        frameInterval = _frameInterval.Interval;
    }
}