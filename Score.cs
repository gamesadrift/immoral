using Photon.Pun;
using UnityEngine;

// Clase para mantener las puntuaciones.
public class Score : MonoBehaviour, IPunObservable
{
    // Muestra puntuación en objeto.
    [SerializeField] private GameObject floatingText;

    // Para pasar por Photon.
    [SerializeField] public int Good { get; set; }
    [SerializeField] public int Bad { get; set; }

    // Componentes necesarias.
    private Character goodGuy = null;
    private Character badGuy = null;
    private GameLogic logic = null;

    // Otras variables.
    private int factorGood;
    private int factorBad;

    private object lockGood = new object();
    private object lockBad = new object();

    void Start()
    {
        // Buscamos en escena las componentes necesarias.
        goodGuy = GameObject.Find("GoodGuy").GetComponent<Character>();
        badGuy = GameObject.Find("BadGuy").GetComponent<Character>();
        logic = GameObject.Find("GameLogic").GetComponent<GameLogic>();

        // Valores por defecto.
        Good = 0;
        Bad = 0;

        factorGood = 1;
        factorBad = 1;
    }

    void Update()
    {
        // Factores decididos segun si tienen o no pocion de moral.
        factorGood = goodGuy.DoubleMoral ? 2 : 1;
        factorBad = badGuy.DoubleMoral ? 2 : 1;
    }

    // Puntos de moral buena.
    public void GoodPoints(int value, Vector3 position)
    {
        if (!logic.MatchEnded)
        {
            // Calculamos puntos.
            int score = value * factorGood;
            // Mostramos los puntos.
            ShowFloatingScore(true, score, position);
            // El MasterClient suma puntos.
            if (PhotonNetwork.IsMasterClient)
                lock (lockGood) Good += score;
        }
    }

    // Puntos de moral mala.
    public void BadPoints(int value, Vector3 position)
    {
        // Similar a GoodPoints.
        if (!logic.MatchEnded)
        {
            int score = value * factorBad;
            ShowFloatingScore(false, score, position);
            if (PhotonNetwork.IsMasterClient)
                lock (lockBad) Bad += score;
        }
    }

    // Mostramos puntos.
    void ShowFloatingScore(bool good, int value, Vector3 position)
    {
        // Creamos el FloatingText y conseguimos componentes.
        GameObject text = Instantiate(floatingText, position, Quaternion.identity, transform);
        TextMesh mesh = text.GetComponent<TextMesh>();
        FloatingText ft = text.GetComponent<FloatingText>();

        // Texto.
        mesh.text = "+" + value;

        // Color y sonido.
        if (good)
        {
            mesh.color = ft.goodColor;
            ft.goodSound.Play();
        }
        else
        {
            mesh.color = ft.badColor;
            ft.badSound.Play();
        }
    }

    // Paso de valores por stream.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Escribimos.
        if (stream.IsWriting)
        {
            stream.SendNext(Good);
            stream.SendNext(Bad);
        }
        // Leemos.
        else
        {
            Good = (int)stream.ReceiveNext();
            Bad = (int)stream.ReceiveNext();
        }
    }
}
