using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject inputPanel = null;
    [SerializeField] private GameObject waitingPanel = null;
    [SerializeField] private TextMeshProUGUI waitingText = null;
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private Button cancelBtn = null;

    private bool connecting = false;

    private const string version = "0.1";
    private const int MaxPlayersInRoom = 2;

    private bool waitingGood = false;
    private bool connectedToMaster = false;
    private float timeToStart = 5.0f;
    private float timeDelay = 0.0f;
    private bool countingDownDone = false;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    private void Update()
    {
        cancelBtn.interactable = connectedToMaster;

        bool cond = timeDelay > 0.0f;
        timerText.enabled = cond;

        if(cond)
        {
            int intTime = (int)Math.Ceiling(timeDelay);
            timerText.text = intTime.ToString();
            timeDelay -= Time.deltaTime;
            countingDownDone = true;
        }
        else
        {
            if (countingDownDone)
            {
                timeDelay = 0.0f;
                countingDownDone = false;
                if (PhotonNetwork.IsMasterClient)
                {
                    float rnd = UnityEngine.Random.Range(0.0f, 9.0f);

                    if (rnd < 5.0f) PhotonNetwork.LoadLevel("Forest");
                    else PhotonNetwork.LoadLevel("Town");
                }
            }
        }
    }

    public void FindOponnent()
    {
        connecting = true;

        inputPanel.SetActive(false);
        waitingPanel.SetActive(true);

        waitingText.text = "Searching...";

        if (PhotonNetwork.IsConnected) PhotonNetwork.JoinRandomRoom();
        else
        {
            Debug.Log("Connecting to master...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = version;
        }
    }

    public void Leave()
    {
        connecting = false;

        timeDelay = 0.0f;
        countingDownDone = false;

        waitingPanel.SetActive(false);
        inputPanel.SetActive(true);

        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        connectedToMaster = true;
        Debug.Log("Connected to master!");
        if (connecting) PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingPanel.SetActive(false);
        inputPanel.SetActive(true);

        Debug.Log($"Disconnected due to: {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms found! Creating one...");

        RoomOptions roomOpts = new RoomOptions();
        roomOpts.IsOpen = true;
        roomOpts.IsVisible = true;
        roomOpts.MaxPlayers = MaxPlayersInRoom;

        float rnd = UnityEngine.Random.Range(0.0f, 9.0f);
        if (rnd < 5.0f)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "team", "bad" } });
            waitingGood = true;
        }
        else
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "team", "good" } });

        PhotonNetwork.CreateRoom(null, roomOpts);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room!");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        
        if(playerCount != MaxPlayersInRoom)
        {
            waitingText.text = "Waiting for an Opponent";
            Debug.Log("Waiting Opponent.");
        }
        else
        {
            timeDelay = timeToStart;
            waitingText.text = "Opponent Found";
            Debug.Log("Match ready.");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersInRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            timeDelay = timeToStart;
            waitingText.text = "Opponent Found";
            Debug.Log("Match ready.");

            if(waitingGood)
                newPlayer.SetCustomProperties(new Hashtable() { { "team", "good" } });
            else
                newPlayer.SetCustomProperties(new Hashtable() { { "team", "bad" } });
        } 
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.CurrentRoom.IsOpen = true;
        base.OnPlayerLeftRoom(otherPlayer);
        timeDelay = 0.0f;
        countingDownDone = false;
        waitingText.text = "Waiting for an Opponent";
    }
}
