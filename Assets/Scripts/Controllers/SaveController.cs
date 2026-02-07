using System;

public class SaveController<T> where T : class, new()
{
    private T _data;
    private string _dirPath, _fileName, _encryptionCodeWord;
    private JsonFileHandler _fileHandler = new();

    internal Action<T> onLoad;
    internal RefAction<T> onSave;
    
    public SaveController(string dirPath, string fileName, string encryptionCodeWord = "")
    {
        _dirPath = dirPath;
        _fileName = fileName;
        _encryptionCodeWord = encryptionCodeWord;
    }

    internal void Load()
    {
        _data = _fileHandler.Load<T>(_dirPath, _fileName, _encryptionCodeWord);
        _data ??= new();
        onLoad?.Invoke(_data);
    }
    internal void Save()
    {
        _data ??= new();
        onSave?.Invoke(ref _data);
        _fileHandler.Save(_dirPath, _fileName, _encryptionCodeWord, _data);
    }
}

public delegate void RefAction<T>(ref T obj);