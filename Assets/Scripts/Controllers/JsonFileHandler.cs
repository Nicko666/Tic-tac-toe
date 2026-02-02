using System.IO;
using System;
using UnityEngine;

public class JsonFileHandler
{
    public T Load<T>(string dataDirPath, string dataFileName, string encryptionCodeWord) where T : class
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        T loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        dataToLoad = streamReader.ReadToEnd();
                    }
                }

                if (encryptionCodeWord != "")
                {
                    dataToLoad = EncryptDecrypt(dataToLoad, encryptionCodeWord);
                }

                loadedData = JsonUtility.FromJson<T>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save<T>(string dataDirPath, string dataFileName, string encryptionCodeWord, T localData) where T : class
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(localData, true);

            if (encryptionCodeWord != "")
            {
                dataToStore = EncryptDecrypt(dataToStore, encryptionCodeWord);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data, string encryptionCodeWord)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}