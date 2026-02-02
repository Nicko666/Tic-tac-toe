using System;

public class VolumeController
{
    private const float Min = 0f;
    private const float Max = 1f;

    private float _value;

    internal float Get() => _value;

    internal void Load(float value)
    {
        _value = value;
        Math.Clamp( _value, Min, Max );
    }

    internal void Save(ref float value)
    {
        value = _value;
    }

    internal void Set(float value)
    {
        _value = value;
        Math.Clamp(_value, Min, Max);
    }
}
