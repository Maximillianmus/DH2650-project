using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayer(GameObject player, string sceneName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = $"{Application.persistentDataPath}/{sceneName}.yarrharr";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Save file to " + path);
    }

    public static LevelData LoadPlayer(string sceneName)
    {
        string path = $"{Application.persistentDataPath}/{sceneName}.yarrharr";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
