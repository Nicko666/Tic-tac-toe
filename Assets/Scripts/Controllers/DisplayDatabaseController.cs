using System;
using System.Collections.Generic;
using UnityEngine;

internal class DisplayDatabaseController
{
    private const int MinFramesCount = 30;

    internal Action<FrameIntervalModel[]> onChanged;

    internal void Set()
    {
        int screenFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;

        List<FrameIntervalModel> frameIntervals = new();

        int interval = 0;
        do
        {
            int framesCount = screenFrameRate / (interval + 1);
            frameIntervals.Add(new(interval, framesCount));
            interval++;
        }
        while (screenFrameRate / (interval + 1) >= MinFramesCount);

        onChanged.Invoke(frameIntervals.ToArray());
    }
}