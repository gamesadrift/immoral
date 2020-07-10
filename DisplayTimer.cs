using System;
using TMPro;
using UnityEngine;

public class DisplayTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer = null;
    [SerializeField] private GameLogic logic = null;

    public float RemainingTime { get; private set; }

    void Start() => RemainingTime = 180;

    void Update()
    {
        int intTime = (int)Math.Ceiling(RemainingTime);
        int min = intTime / 60;
        int sec = intTime % 60;
        timer.text = min + ":" + ((sec < 10) ? "0" : "") + sec;

        if (!logic.MatchEnded)
            if (intTime > 0) RemainingTime -= Time.deltaTime;
    }
}
