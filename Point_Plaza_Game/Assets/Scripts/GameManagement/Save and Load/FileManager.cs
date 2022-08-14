using System.IO;
using System;
using UnityEngine;

public static class FileManager
{
    public const string SAVE_DATA_FILE_NAME = "SaveData.dat";

    public static bool WriteToFile(string fileName, string fileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            File.WriteAllText(fullPath, fileContents);
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
        }

        return false;
    }
    public static bool LoadFromFile(string fileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}
