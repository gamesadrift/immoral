using Photon.Pun;
using UnityEngine;

// Clase para lógica de partida.
public class GameLogic : MonoBehaviour
{
    // Interfaz
    [SerializeField] private GameObject UIGame = null;

    // Tiempo
    private DisplayTimer timer;

    // Propiedades que usa para comunicarse.
    public bool MatchEnded { get; private set; }
    public byte Reason { get; private set; }

    void Start()
    {
        // Consigue el contador.
        timer = UIGame.GetComponent<DisplayTimer>();
        // La partida no ha acabado.
        MatchEnded = false;
    }
    void Update()
    {
        // Si no ha acabado comprueba si lo ha hecho.
        if (!MatchEnded)
        {
            // Razón 0: Se acabó el tiempo.
            if (timer.RemainingTime <= 0)
            {
                MatchEnded = true;
                Reason = 0;
            }

            // Razón 1: Desconexión.
            if (PhotonNetwork.PlayerListOthers.Length == 0)
            {
                MatchEnded = true;
                Reason = 1;
            }
        }
    }
}
