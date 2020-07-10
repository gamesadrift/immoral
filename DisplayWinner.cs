using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayWinner : MonoBehaviour
{
    [SerializeField] private GameLogic logic = null;
    [SerializeField] private Score score = null;
    [SerializeField] private TextMeshProUGUI winner = null;

    [SerializeField] private GameObject MusicGoodWins = null;
    [SerializeField] private GameObject MusicBadWins = null;
    [SerializeField] private GameObject MusicTie = null;


    private GameObject GoodGuy = null;
    private GameObject BadGuy = null;

    private string winText;

    private bool win, showed;

    void Start()
    {
        GoodGuy = GameObject.Find("GoodGuy");
        BadGuy = GameObject.Find("BadGuy");

        win = false;
        showed = false;
        winText = "Tied Game!";
    }

    void Update()
    {
        if (logic.MatchEnded && !showed)
        {
            GameObject music = GameObject.Find("MusicGood(Clone)");
            if (music != null) Destroy(music);
            music = GameObject.Find("MusicBad(Clone)");
            if (music != null) Destroy(music);

            if(logic.Reason == 0)
            {
                if (score.Good > score.Bad)
                {
                    Instantiate(MusicGoodWins);
                    winText = GoodGuy.GetComponent<PhotonView>().Owner.NickName;
                    win = true;
                }
                else if (score.Bad > score.Good)
                {
                    winText = BadGuy.GetComponent<PhotonView>().Owner.NickName;
                    Instantiate(MusicBadWins);
                    win = true;
                }

                winner.text = winText + ((win) ? " wins!" : "");
            }

            if (logic.Reason == 1)
            {
                bool isGood = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "good";
                if(isGood) Instantiate(MusicGoodWins);
                else Instantiate(MusicBadWins);
                winner.text = "Opponent disconnected!";
            }

            Instantiate(MusicTie);

            winner.enabled = true;
            winner.GetComponent<Animator>().Play("WinnerAnimation");

            showed = true;
        }
    }
}
