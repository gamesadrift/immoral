using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

// Clase para mostrar las puntuaciones.
public class DisplayScore : MonoBehaviour
{
    // Campos a rellenar.
    [SerializeField] private TextMeshProUGUI GoodGuy = null;    // Izq. Top
    [SerializeField] private TextMeshProUGUI GoodScore = null;  // Izq. Debajo 
    [SerializeField] private TextMeshProUGUI BadGuy = null;     // Der. Top
    [SerializeField] private TextMeshProUGUI BadScore = null;   // Der. Debajo

    // Objeto con la puntuación.
    [SerializeField] private Score score = null;

    void Start()
    {
        // Para cada jugador:
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            // Si es Bernie pone su nombre a la izquierda.
            if ((string)p.CustomProperties["team"] == "good")
                GoodGuy.text = p.NickName;
            // Si es Jonas lo pone a la derecha.
            else BadGuy.text = p.NickName;
        }

        // Pone las puntuaciones en sus respectivos campos.
        GoodScore.text = score.Good.ToString();
        BadScore.text = score.Bad.ToString();
    }

    void Update()
    {
        // Actualiza las puntuaciones cada frame.
        GoodScore.text = score.Good.ToString();
        BadScore.text = score.Bad.ToString();
    }
}
