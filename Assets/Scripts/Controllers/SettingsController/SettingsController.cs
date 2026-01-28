using System;
using UnityEngine;

internal class SettingsController
{
    private SettingsModel _settings = new();
    private SettingsDatabase _appDatabase = new();

    internal Action<SettingsModel> onSettingsChanged;
    internal Action onInputSaveSettings;

    internal void SetDatabase(in SettingsDatabase appDatabase) =>
        _appDatabase = appDatabase;

    internal void SetSettings(SettingsModel settings) 
    {
        _settings.volume = settings.volume;

        _settings.frameInteravl = 
            Array.Exists(_appDatabase.FrameIntervals, i => i.Interval == settings.frameInteravl.Interval) ? 
            settings.frameInteravl : _appDatabase.FrameIntervals[0];

        if (Application.targetFrameRate != _settings.frameInteravl.FramesCount)
            Application.targetFrameRate = _settings.frameInteravl.FramesCount;

        onInputSaveSettings.Invoke();
        onSettingsChanged?.Invoke(_settings);
    }
    internal void GetSettings(ref SettingsModel settings)
    {
        settings.volume = _settings.volume;
        settings.frameInteravl = _settings.frameInteravl;
    }
}