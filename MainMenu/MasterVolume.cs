using UnityEngine;
using UnityEngine.UI;

// Clase para control de volumen maestro
public class MasterVolume : MonoBehaviour
{
    // Barra de selección de volumen
    [SerializeField] private Slider masterVolumeSlider = null;

    // Claves para las preferencias
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MasterVolumePref = "MasterVolumePref";

    // Variable para guardar el volumen (auxiliar)
    private float masterVolume;

    private void Awake()
    {
        // ¿Es la primera vez que juega?
        int fp = PlayerPrefs.GetInt(FirstPlay);

        if(fp == 0)
        {
            // Si, valor por defecto.
            masterVolume = 1.0f;
            masterVolumeSlider.value = masterVolume;
            PlayerPrefs.SetFloat(MasterVolumePref, masterVolume);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            // No, valor obtenido de las preferencias.
            masterVolume = PlayerPrefs.GetFloat(MasterVolumePref);
            masterVolumeSlider.value = masterVolume;
        }
    }

    // En cada frame se pone el valor guardado.
    void Update() => AudioListener.volume = masterVolume;

    // Cuando se actualiza el slider.
    public void SetVolume(float vol)
    {
        // Se actualiza el valor y las preferencias.
        masterVolume = vol;
        PlayerPrefs.SetFloat(MasterVolumePref, vol);
    }

    // Para guardar en preferencias.
    private void SaveSoundSettings() => PlayerPrefs.SetFloat(MasterVolumePref, masterVolume);

    private void OnApplicationFocus(bool focus)
    {
        // Si se pierde el focus (cierre o minimizar), se guarda.
        if (!focus) SaveSoundSettings();
    }
}
