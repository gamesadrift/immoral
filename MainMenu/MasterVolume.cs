using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider = null;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MasterVolumePref = "MasterVolumePref";

    private float masterVolume;

    private void Awake()
    {
        int fp = PlayerPrefs.GetInt(FirstPlay);
        if(fp == 0)
        {
            masterVolume = 1.0f;
            masterVolumeSlider.value = masterVolume;
            PlayerPrefs.SetFloat(MasterVolumePref, masterVolume);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            masterVolume = PlayerPrefs.GetFloat(MasterVolumePref);
            masterVolumeSlider.value = masterVolume;
        }
    }

    void Update() => AudioListener.volume = masterVolume;

    public void SetVolume(float vol)
    {
        masterVolume = vol;
        PlayerPrefs.SetFloat(MasterVolumePref, vol);
    }

    private void SaveSoundSettings() => PlayerPrefs.SetFloat(MasterVolumePref, masterVolume);

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveSoundSettings();
    }
}
