using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Clase para conexión
public class JoinRoom : MonoBehaviourPunCallbacks
{
    // Paneles para el flujo de conexión (input > esperar oponente)
    [SerializeField] private GameObject inputPanel = null;
    [SerializeField] private GameObject waitingPanel = null;
    [SerializeField] private TextMeshProUGUI waitingText = null;
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private Button cancelBtn = null;

    // ¿Estamos conectando?
    private bool connecting = false;

    // Configuracion, version y jugadores por sala.
    private const string version = "0.2";
    private const int MaxPlayersInRoom = 2;

    // Variables para control.
    private bool waitingGood = false;
    private bool connectedToMaster = false;
    private float timeToStart = 5.0f;
    private float timeDelay = 0.0f;
    private bool countingDownDone = false;

    // Las escenas están sincronizadas.
    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    private void Update()
    {
        // Solo al conectarnos a Photon podemos cancelar la busqueda/espera.
        cancelBtn.interactable = connectedToMaster;

        // Para mostrar el tiempo de empezar partida.
        bool cond = timeDelay > 0.0f;
        timerText.enabled = cond;

        if(cond)
        {
            // Lo mostramos y vamos reduciendo.
            int intTime = (int)Math.Ceiling(timeDelay);
            timerText.text = intTime.ToString();
            timeDelay -= Time.deltaTime;
            countingDownDone = true;
        }
        else
        {
            if (countingDownDone)
            {
                // Si se ha completado la cuenta atrás.

                // Reset.
                timeDelay = 0.0f;
                countingDownDone = false;

                // MasterClient lanza la escena (al azar).
                if (PhotonNetwork.IsMasterClient)
                {
                    float rnd = UnityEngine.Random.Range(0.0f, 9.0f);

                    if (rnd < 5.0f) PhotonNetwork.LoadLevel("Forest");
                    else PhotonNetwork.LoadLevel("Town");
                }
            }
        }
    }

    // Para buscar oponente.
    public void FindOponnent()
    {
        // Estamos conectando.
        connecting = true;

        // Pasamos al panel de espera.
        inputPanel.SetActive(false);
        waitingPanel.SetActive(true);

        // Buscando sala
        waitingText.text = "Searching...";

        // Si estamos conectados intentamos unirnos a sala
        if (PhotonNetwork.IsConnected) PhotonNetwork.JoinRandomRoom();
        else
        {
            // Si no conectamos a master con la configuracion dada.
            Debug.Log("Connecting to master...");
            PhotonNetwork.GameVersion = version;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Para salir.
    public void Leave()
    {
        // Ya no estamos conectando.
        connecting = false;

        // Reset del timer.
        timeDelay = 0.0f;
        countingDownDone = false;

        // Volvemos al panel de input.
        waitingPanel.SetActive(false);
        inputPanel.SetActive(true);

        // Salimos de la sala.
        PhotonNetwork.LeaveRoom();
    }

    // Cuando conectamos.
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        connectedToMaster = true;
        Debug.Log("Connected to master!");

        // Intentamos unirnos a sala.
        if (connecting) PhotonNetwork.JoinRandomRoom();
    }

    // Por desconexión.
    public override void OnDisconnected(DisconnectCause cause)
    {
        // Reset del timer.
        timeDelay = 0.0f;
        countingDownDone = false;

        // Volvemos al panel de input.
        waitingPanel.SetActive(false);
        inputPanel.SetActive(true);

        Debug.Log($"Disconnected due to: {cause}");
    }

    // Si falla unirnos a sala.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms found! Creating one...");

        // Opciones de la sala.
        RoomOptions roomOpts = new RoomOptions();
        roomOpts.IsOpen = true;
        roomOpts.IsVisible = true;
        roomOpts.MaxPlayers = MaxPlayersInRoom;

        // Nos asignamos bueno o malo (azar).
        float rnd = UnityEngine.Random.Range(0.0f, 9.0f);
        if (rnd < 5.0f)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "team", "bad" } });
            waitingGood = true; // Esperamos al bueno.
        }
        // Por defecto esperamos al malo.
        else PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "team", "good" } });

        // Creamos la sala.
        PhotonNetwork.CreateRoom(null, roomOpts);
    }

    // Al unirnos a una sala.
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room!");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        
        if(playerCount != MaxPlayersInRoom)
        {
            // Si falta alguien esperamos.
            waitingText.text = "Waiting for an Opponent";
            Debug.Log("Waiting Opponent.");
        }
        else
        {
            // Si no empezamos el timer al darle valor.
            timeDelay = timeToStart;
            waitingText.text = "Opponent Found";
            Debug.Log("Match ready.");
        }
    }

    // Si entra alguien a nuestra sala.
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersInRoom)
        {
            // Cerramos sala y preparamos el timer.
            PhotonNetwork.CurrentRoom.IsOpen = false;
            timeDelay = timeToStart;
            waitingText.text = "Opponent Found";
            Debug.Log("Match ready.");

            // Le asignamos lo que esperabamos.
            if(waitingGood)
                newPlayer.SetCustomProperties(new Hashtable() { { "team", "good" } });
            else
                newPlayer.SetCustomProperties(new Hashtable() { { "team", "bad" } });
        } 
    }

    // Si alguien se va
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Abrimos sala, reset del timer y volvemos a esperar.
        PhotonNetwork.CurrentRoom.IsOpen = true;
        base.OnPlayerLeftRoom(otherPlayer);
        timeDelay = 0.0f;
        countingDownDone = false;
        waitingText.text = "Waiting for an Opponent";
    }
}
