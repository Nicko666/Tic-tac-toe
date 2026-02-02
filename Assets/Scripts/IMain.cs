using System;
using UnityEngine;

internal abstract class IMain : MonoBehaviour
{
    internal abstract event Action<SoundModel> onInputSound;
    internal abstract event Action<SceneModel> onInputScene;
    internal abstract event Action<SettingsOutputModel> onInputSettings;
    internal abstract event Action onInputSaveProgress;

    internal abstract void OutputProgressDatabase(ProgressDatabase database);
    internal abstract void OutputFrameIntervals(FrameIntervalModel[] frameIntervals);
    internal abstract void OutputLoadedProgressData(ProgressData data);
    internal abstract void OutputSavedProgressData(ref ProgressData data);
    internal abstract void OutputPlayers(PlayerModel[] players);
    internal abstract void OutputRules(RulesModel rules);
    internal abstract void OutputSettings(SettingsOutputModel model);
}
