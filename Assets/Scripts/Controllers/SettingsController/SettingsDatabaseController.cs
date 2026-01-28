using System.Collections.Generic;
using UnityEngine;

internal class SettingsDatabaseController
{
    private FrameIntervalsController _framesController = new();

    internal SettingsDatabase Get()
    {
        FrameIntervalModel[] frameIntervalModel = _framesController.Get();

        return new(frameIntervalModel);
    }

    class FrameIntervalsController
    {
        private const int MaxFrameInterval = 2;

        internal FrameIntervalModel[] Get()
        {
            int screenFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
            List<FrameIntervalModel> frameIntervals = new();

            for (int interval = 0; interval < MaxFrameInterval; interval++)
            {
                int framesCount = screenFrameRate / (interval + 1);
                frameIntervals.Add(new(interval, framesCount));
            }

            return frameIntervals.ToArray();
        }
    }
}