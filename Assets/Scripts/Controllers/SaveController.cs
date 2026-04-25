public class SaveController<T> where T : class, new()
{
    private T _data;
    private string _dirPath, _fileName, _encryptionCodeWord;
    private JsonFileHandler _fileHandler = new();

    public SaveController(string dirPath, string fileName, string encryptionCodeWord = "")
    {
        _dirPath = dirPath;
        _fileName = fileName;
        _encryptionCodeWord = encryptionCodeWord;
    }

    internal T Load()
    {
        _data = _fileHandler.Load<T>(_dirPath, _fileName, _encryptionCodeWord);
        _data ??= new();
        return _data;
    }
    internal void Save(T data)
    {
        _data ??= new();
        _fileHandler.Save(_dirPath, _fileName, _encryptionCodeWord, _data);
    }
}

public delegate void RefAction<T>(ref T obj);