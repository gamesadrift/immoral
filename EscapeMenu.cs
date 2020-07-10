using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject escapePanel = null;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            escapePanel.SetActive(!escapePanel.activeSelf);
    }

    public void Leave() => PhotonNetwork.LeaveRoom();

    public void Cancel() => escapePanel.SetActive(false);

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
