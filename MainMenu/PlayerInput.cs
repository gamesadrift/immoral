using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Clase para el input (nombre) del jugador
public class PlayerInput : MonoBehaviour
{
    // Campo de nombre
    [SerializeField] private TMP_InputField nameTxt = null;
    // Boton para buscar partida (solo se activa aqui)
    [SerializeField] private Button findBtn = null;

    // Clave para preferencia de nombre
    private const string prefNameKey = "PlayerName";

    // Al empezar...
    private void Start() => SetupInputField();
    // coloca el nombre sacado de preferencias...
    private void SetupInputField()
    {
        // si existe...
        if (!PlayerPrefs.HasKey(prefNameKey)) return;

        // Lo obtiene.
        string name = PlayerPrefs.GetString(prefNameKey);

        // Lo pone (red y preferencias).
        PhotonNetwork.NickName = name;
        nameTxt.text = name;

        // Lo guarda (por si acaso).
        SetPlayerName(name);
    }

    // Cuando se actualiza el campo.
    public void SetPlayerName(string name)
    {
        // Guarda el nombre (red y preferencias).
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString(prefNameKey, name);

        // Activa el botón si tiene algun nombre.
        findBtn.interactable = !string.IsNullOrEmpty(name);
    }

}
