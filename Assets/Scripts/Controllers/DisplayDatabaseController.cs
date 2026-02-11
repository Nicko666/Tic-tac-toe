using System;
using System.Collections.Generic;
using UnityEngine;

internal class DisplayDatabaseController
{
    private const int MinFramesCount = 30;

    internal Action<FrameIntervalModel[]> onChanged;

    internal void Set()
    {
        int screenFrameRate = 0;

        if (Screen.resolutions.Length > 0)
            screenFrameRate = (int)Screen.resolutions[^1].refreshRateRatio.value;

        List<FrameIntervalModel> frameIntervals = new();

        if (screenFrameRate > 0)
        {
            int interval = 0;
            do
            {
                int framesCount = screenFrameRate / (interval + 1);
                frameIntervals.Add(new(interval, framesCount));
                interval++;
            }
            while (screenFrameRate / (interval + 1) >= MinFramesCount);
        }
        else
        {
            frameIntervals.Add(new(0, (int)Screen.currentResolution.refreshRateRatio.value));
        }

        onChanged?.Invoke(frameIntervals.ToArray());
    }
}