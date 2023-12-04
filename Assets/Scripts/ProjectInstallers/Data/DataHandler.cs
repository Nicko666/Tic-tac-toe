using System;
using System.IO;
using UnityEngine;

public class DataHandler<T> where T : class
{
    public T Load(string dataFileName)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, dataFileName);

        T data = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                data = JsonUtility.FromJson<T>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load data \n" + e);
            }
        }

        return data;

    }
    
    public void Save(T data, string dataFileName)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

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
            Debug.LogError("Failed to save data \n" + e);
        }

    }


}
