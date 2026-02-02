using System;

[Serializable]
public struct SettingsOutputModel
{
    public float volume;
    public FrameIntervalModel frameInteravl;

    public SettingsOutputModel(float volume, FrameIntervalModel frameInteravl)
    {
        this.volume = volume;
        this.frameInteravl = frameInteravl;
    }
}