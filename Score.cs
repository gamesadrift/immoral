using Photon.Pun;
using UnityEngine;

public class Score : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject floatingText;

    [SerializeField] public int Good { get; set; }
    [SerializeField] public int Bad { get; set; }

    private Character goodGuy = null;
    private Character badGuy = null;

    private GameLogic logic;

    private int factorGood;
    private int factorBad;

    private object lockGood = new object();
    private object lockBad = new object();

    void Start()
    {
        goodGuy = GameObject.Find("GoodGuy").GetComponent<Character>();
        badGuy = GameObject.Find("BadGuy").GetComponent<Character>();

        Good = 0;
        Bad = 0;

        factorGood = 1;
        factorBad = 1;

        logic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }

    void Update()
    {
        factorGood = goodGuy.DoubleMoral ? 2 : 1;
        factorBad = badGuy.DoubleMoral ? 2 : 1;
    }

    public void GoodPoints(int value, Vector3 position)
    {
        if (!logic.MatchEnded)
        {
            int score = value * factorGood;
            ShowFloatingScore(true, score, position);
            if (PhotonNetwork.IsMasterClient)
                lock (lockGood) Good += score;
        }
    }

    public void BadPoints(int value, Vector3 position)
    {
        if (!logic.MatchEnded)
        {
            int score = value * factorBad;
            ShowFloatingScore(false, score, position);
            if (PhotonNetwork.IsMasterClient)
                lock (lockBad) Bad += score;
        }
    }

    void ShowFloatingScore(bool good, int value, Vector3 position)
    {
        GameObject text = Instantiate(floatingText, position, Quaternion.identity, transform);
        TextMesh mesh = text.GetComponent<TextMesh>();
        mesh.text = "+" + value;
        FloatingText ft = text.GetComponent<FloatingText>();
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Good);
            stream.SendNext(Bad);
        }
        else
        {
            Good = (int)stream.ReceiveNext();
            Bad = (int)stream.ReceiveNext();
        }
    }
}
