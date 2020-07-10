using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameTxt = null;
    [SerializeField] private Button findBtn = null;

    private const string prefNameKey = "PlayerName";

    private void Start() => SetupInputField();

    private void SetupInputField()
    {
        if (!PlayerPrefs.HasKey(prefNameKey)) return;

        string name = PlayerPrefs.GetString(prefNameKey);

        PhotonNetwork.NickName = name;
        nameTxt.text = name;

        SetPlayerName(name);
    }

    public void SetPlayerName(string name)
    {
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString(prefNameKey, name);

        findBtn.interactable = !string.IsNullOrEmpty(name);
    }

}
