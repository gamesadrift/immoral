using Photon.Pun;
using TMPro;
using UnityEngine;

// Clase para mostrar quién ganó.
public class DisplayWinner : MonoBehaviour
{
    [SerializeField] private GameLogic logic = null;            // Objeto con la lógica.
    [SerializeField] private Score score = null;                // Objeto con puntuación.
    [SerializeField] private TextMeshProUGUI winner = null;     // Texto para mensaje victoria.

    [SerializeField] private GameObject MusicGoodWins = null;   // Música si gana Bernie (Bueno).
    [SerializeField] private GameObject MusicBadWins = null;    // Música si gana Jonas (Malo).

    // Música de empate, desconexión o que suena de fondo tras una de las anteriores.
    [SerializeField] private GameObject MusicTie = null;         

    // Personajes, Bernie y Jonas, Bueno y Malo.
    private GameObject GoodGuy = null;
    private GameObject BadGuy = null;

    // Mensaje a mostrar.
    private string winText;
    // Para control de estados.
    private bool win, showed;

    void Start()
    {
        // Buscamos en la escena.
        GoodGuy = GameObject.Find("GoodGuy");
        BadGuy = GameObject.Find("BadGuy");

        // Estado inicial
        win = false;            // No ganó nadie.
        showed = false;         // No se ha mostrado el mensaje.
        winText = "Tied Game!"; // Se asume empate.
    }

    void Update()
    {
        // Si ha acabado la partida y no se habia mostrado el mensaje.
        if (logic.MatchEnded && !showed)
        {
            // Se para la musica para ambos.
            GameObject music = GameObject.Find("MusicGood(Clone)");
            if (music != null) Destroy(music);
            music = GameObject.Find("MusicBad(Clone)");
            if (music != null) Destroy(music);

            // Razón 0: Se acabó el tiempo.
            if(logic.Reason == 0)
            {
                // Si Bernie tiene más puntos
                if (score.Good > score.Bad)
                {
                    // Música si gana Bernie
                    Instantiate(MusicGoodWins);
                    // El nombre del jugador que controlaba a Bernie.
                    winText = GoodGuy.GetComponent<PhotonView>().Owner.NickName;
                    // Alguien ganó.
                    win = true;
                }
                // Viceversa para Jonas.
                else if (score.Bad > score.Good)
                {
                    Instantiate(MusicBadWins);
                    winText = BadGuy.GetComponent<PhotonView>().Owner.NickName;
                    win = true;
                }

                // Resto del texto. Si no ganó nadie es el por defecto.
                winner.text = winText + ((win) ? " wins!" : "");
            }

            // Razón 1: Desconexión
            if (logic.Reason == 1)
            {
                // Determinamos quien queda y ponemos su música de victoria.
                bool isGood = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "good";
                if(isGood) Instantiate(MusicGoodWins);
                else Instantiate(MusicBadWins);

                // Texto de que se desconectó.
                // Podría ponerse "[NickName] wins!" pero optamos por esto.
                winner.text = "Opponent disconnected!";
            }

            // Por defecto y de fondo queda la música de empate (la del menú principal).
            Instantiate(MusicTie);

            // Mostramos el panel con su animación.
            winner.enabled = true;
            winner.GetComponent<Animator>().Play("WinnerAnimation");

            // Se ha mostrado la victoria.
            showed = true;
        }
    }
}
