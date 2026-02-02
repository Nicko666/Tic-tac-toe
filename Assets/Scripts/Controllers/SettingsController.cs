using System;

internal class SettingsController
{
    private FrameIntervalController _frameIntervalController = new();
    private VolumeController _volumeController = new();

    private SettingsOutputModel _settingsOutput;
    
    internal Action<SettingsOutputModel> onSettingsChanged;
    internal Action onInputSaveSettings;

    internal void LoadSettingsData(SettingsData data)
    {
        _volumeController.Load(data.volume);
        _settingsOutput.volume = _volumeController.Get();

        _frameIntervalController.Load(data.frameInterval);
        _settingsOutput.frameInteravl = _frameIntervalController.Get();

        onSettingsChanged.Invoke(_settingsOutput);
    }
    internal void SaveSettingsData(ref SettingsData data)
    {
        _volumeController.Save(ref data.volume);
        _frameIntervalController.Save(ref data.frameInterval);
    }

    internal void SetDisplayDatabase(in FrameIntervalModel[] frameIntervals)
    {
        _frameIntervalController.SetDatabase(frameIntervals);
        _settingsOutput.frameInteravl = _frameIntervalController.Get();

        onSettingsChanged.Invoke(_settingsOutput);
    }

    internal void SetSettings(SettingsOutputModel settings) 
    {
        _volumeController.Set(settings.volume);
        _settingsOutput.volume = _volumeController.Get();

        _frameIntervalController.Set(settings.frameInteravl);
        _settingsOutput.frameInteravl = _frameIntervalController.Get();
        
        onInputSaveSettings.Invoke();
        onSettingsChanged.Invoke(_settingsOutput);
    }
}