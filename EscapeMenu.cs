using Photon.Pun;
using UnityEngine;

// Clase para el menú que aparece al pulsar escape.
public class EscapeMenu : MonoBehaviourPunCallbacks
{
    // Panel donde está el menú
    [SerializeField] private GameObject escapePanel = null;

    void Update()
    {
        // Si se pulsa escape hace toggle entre mostrar y no.
        if (Input.GetKeyDown("escape"))
            escapePanel.SetActive(!escapePanel.activeSelf);
    }

    // Si se da a YES, se sale de la sala.
    public void Leave() => PhotonNetwork.LeaveRoom();

    // Si se da a NO se cierra el panel (también puede cerrarse con escape).
    public void Cancel() => escapePanel.SetActive(false);

    // Cuando ha terminado de salir carga el menú principal.
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
