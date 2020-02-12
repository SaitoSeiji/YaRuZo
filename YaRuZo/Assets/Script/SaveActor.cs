using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveActor<T>
{
    public void InitAction()
    {
        Directory.CreateDirectory(GetApplicationPath());
    }

    public void Save(T data,string path)
    {
        string json = JsonUtility.ToJson(data);

        StreamWriter writer = new StreamWriter(GetApplicationPath()+path+".json");
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public T Load(string path)
    {
        string dataPath = GetApplicationPath() + path + ".json";
        if (!Directory.Exists(GetApplicationPath()))
        {
            InitAction();
        }

        if (!File.Exists(dataPath))
        {
            return default(T);
        }

        StreamReader streamReader = new StreamReader(dataPath);
        string data = streamReader.ReadToEnd();
        streamReader.Close();
        return JsonUtility.FromJson<T>(data);
    }

    string GetApplicationPath()
    {
#if UNITY_EDITOR
        return Application.dataPath+"/SaveData/";
#else
        return Application.persistentDataPath+"/SaveData/";
#endif
    }
}
