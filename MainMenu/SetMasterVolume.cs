using UnityEngine;

// Clase para master volume en las otras escenas.
public class SetMasterVolume : MonoBehaviour
{
    // Clave de preferencia del volumen
    private static readonly string MasterVolumePref = "MasterVolumePref";
    // Variable para el valor (auxiliar)
    private float masterVolume;
    void Awake()
    {
        // Lo sacamos de preferencias y lo asignamos.
        masterVolume = PlayerPrefs.GetFloat(MasterVolumePref);
        AudioListener.volume = masterVolume;
    }
}
