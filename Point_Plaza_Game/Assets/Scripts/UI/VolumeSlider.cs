using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Slider component that adjusts the volume of MixerGroup of a specified name.
/// </summary>
[RequireComponent(typeof(Slider))]
[DisallowMultipleComponent]
public class VolumeSlider : MonoBehaviour
{
    private Slider volSlider = null;
    [SerializeField] private string mixerGroupName = "Master";

    private void Awake()
    {
        volSlider = GetComponent<Slider>();
        volSlider.onValueChanged.AddListener(ChangeVol);
    }

    private void OnDisable()
    {
        if(volSlider != null)
        {
            volSlider.onValueChanged.RemoveListener(ChangeVol);
        }
    }
    private void ChangeVol(float vol)
    {
        AudioManagerSingleton.Instance.SetVol(mixerGroupName, vol);
    }
}
