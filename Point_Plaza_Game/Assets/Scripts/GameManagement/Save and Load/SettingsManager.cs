using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Handles the the saving and loading of game settings such as volume.
/// </summary>
public class SettingsManager : MonoBehaviour
{
    [SerializeField] private (string setting, float value) settings;

    // Start is called before the first frame update
    void Start()
    {
        LoadPreferences();
    }

    void OnApplicationQuit()
    {
        SavePreferences();
    }

    public void SavePreferences()
    {
        foreach (var setting in AudioManagerSingleton.Instance.Mixer.FindMatchingGroups(""))
        {
            float vol;
            AudioManagerSingleton.Instance.Mixer.GetFloat(setting.name, out vol);
            Debug.LogError($"Setting is {setting.name} Value is {vol}");
            PlayerPrefs.SetFloat(setting.name, vol);
        }
        PlayerPrefs.Save();
    }
    public void LoadPreferences()
    {
        foreach(var setting in AudioManagerSingleton.Instance.Mixer.FindMatchingGroups(""))
        {
            var vol = PlayerPrefs.GetFloat(setting.name);
            Debug.LogError($"Setting is {setting.name} Value is {vol}");
            AudioManagerSingleton.Instance.SetVol(setting.name, vol);
        }
    }
}
