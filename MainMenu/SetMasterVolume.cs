using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMasterVolume : MonoBehaviour
{
    private static readonly string MasterVolumePref = "MasterVolumePref";

    private float masterVolume;
    void Awake()
    {
        masterVolume = PlayerPrefs.GetFloat(MasterVolumePref);
        AudioListener.volume = masterVolume;
    }


}
