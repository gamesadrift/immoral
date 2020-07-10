using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject UIGame = null;

    private DisplayTimer timer;

    public bool MatchEnded { get; private set; }
    public byte Reason { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        timer = UIGame.GetComponent<DisplayTimer>();
        MatchEnded = false;
    }
    void Update()
    {
        if (!MatchEnded)
        {
            if (timer.RemainingTime <= 0)
            {
                MatchEnded = true;
                Reason = 0;
            }
            if (PhotonNetwork.PlayerListOthers.Length == 0)
            {
                MatchEnded = true;
                Reason = 1;
            }
        }
    }
}
