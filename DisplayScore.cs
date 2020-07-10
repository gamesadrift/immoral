using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GoodGuy = null;
    [SerializeField] private TextMeshProUGUI GoodScore = null;
    [SerializeField] private TextMeshProUGUI BadGuy = null;
    [SerializeField] private TextMeshProUGUI BadScore = null;
    [SerializeField] private Score score = null;

    [SerializeField] public string GoodGuyName { get; set; }
    [SerializeField] public string BadGuyName { get; set; }

    void Start()
    {
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            if((string)p.CustomProperties["team"] == "good")
                GoodGuy.text = p.NickName;
            else
                BadGuy.text = p.NickName;
        }

        GoodScore.text = score.Good.ToString();
        BadScore.text = score.Bad.ToString();
    }

    void Update()
    {
        GoodScore.text = score.Good.ToString();
        BadScore.text = score.Bad.ToString();
    }
}
