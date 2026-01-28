using System;
using UnityEngine;

internal abstract class IMain : MonoBehaviour
{
    internal abstract event Action<SceneModel> onInputScene;
    internal abstract event Action<SettingsModel> onInputSettings;
    internal abstract event Action onInputSaveProgress;

    internal abstract void OutputProgressDatabase(ProgressDatabase database);
    internal abstract void OutputSettingsDatabase(SettingsDatabase settingsDatabase);
    internal abstract void LoadProgressData(ProgressData data);
    internal abstract void SaveProgressData(ref ProgressData data);
    internal abstract void OutputPlayers(PlayerModel[] players);
    internal abstract void OutputRules(RulesModel rules);
    internal abstract void OutputSettings(SettingsModel model);
}
