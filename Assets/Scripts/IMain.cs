using System;
using UnityEngine;

internal abstract class IMain : MonoBehaviour
{
    internal abstract event Action<SoundModel> onInputSound;
    internal abstract event Action<SceneModel> onInputScene;
    internal abstract event Action<SettingsOutputModel> onInputSettings;
    internal abstract event Action onInputSaveProgress;

    internal abstract void OutputProgressDatabase(ProgressDatabase database);
    internal abstract void OutputSettingsDatabase(FrameIntervalModel[] frameIntervals);
    internal abstract void LoadProgress(ProgressModel data);
    internal abstract void SaveProgress(ref ProgressModel data);
    internal abstract void OutputSettings(SettingsOutputModel model);
}
