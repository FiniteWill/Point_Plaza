using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Slider component that adjusts the volume of MixerGroup of a specified name.
/// </summary>
[RequireComponent(typeof(Slider))]
[DisallowMultipleComponent]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private float minVolume = -80;
    [SerializeField] private float maxVolume = 0;
    [SerializeField] private string mixerGroupName = "Master";

    private Slider volSlider = null;

    public string MixerGroupName => mixerGroupName;

    private void Awake()
    {
        volSlider = GetComponent<Slider>();
        volSlider.onValueChanged.AddListener(ChangeVol);
        volSlider.minValue = minVolume;
        volSlider.maxValue = maxVolume;
    }

    private void Start()
    {
        if(volSlider != null)
        {
            volSlider.value = PlayerPrefs.GetFloat(mixerGroupName);
        }
    }

    private void OnEnable()
    {
        if(volSlider != null)
        {
            volSlider.onValueChanged.AddListener(ChangeVol);
        }
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
