using UnityEngine;

internal class DataController
{
    private JsonFileHandler _fileHandler = new();
    private const string _progressFileName = "Progress", _progressEncryptionCode = "", _settingsFileName = "Settings";

    internal ProgressData LoadProgressData()
    {
        ProgressData data = _fileHandler.Load<ProgressData>(Application.persistentDataPath, _progressFileName, _progressEncryptionCode);
        data ??= new();
        //Debug.Log($"LoadProgressData");
        return data;
    }
    internal void SaveProgressData(ProgressData data)
    {
        data ??= new();
        _fileHandler.Save(Application.persistentDataPath, _progressFileName, _progressEncryptionCode, data);
        //Debug.Log($"SaveProgressData");
    }

    internal SettingsData LoadSettingsData()
    {
        SettingsData data = _fileHandler.Load<SettingsData>(Application.persistentDataPath, _settingsFileName, "");
        data ??= new();
        return data;
    }
    internal void SaveSettingsData(SettingsData data)
    {
        data ??= new();
        _fileHandler.Save(Application.persistentDataPath, _settingsFileName, "", data);
    }
}
