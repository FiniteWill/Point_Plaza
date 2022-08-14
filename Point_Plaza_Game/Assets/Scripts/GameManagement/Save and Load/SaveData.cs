using System;
using UnityEngine;

// Adapted from https://www.youtube.com/watch?v=uD7y4T4PVk0

[Serializable]
public class SaveData
{
    public int hiScore;
    

    public string ToJSon()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string str)
    {
        JsonUtility.FromJsonOverwrite(str, this);
    }
}
