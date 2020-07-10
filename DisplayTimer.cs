using System;
using TMPro;
using UnityEngine;

// Clase para controlar el reloj de la partida
public class DisplayTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer = null;  // Texto para poner el tiempo.
    [SerializeField] private GameLogic logic = null;        // Objeto con la lógica.

    // Propiedad del tiempo que queda.
    public float RemainingTime { get; private set; }

    // Tiempo inicial en segundos
    void Start() => RemainingTime = 180;

    void Update()
    {
        // Se calcula el tiempo que queda en formato correcto
        int intTime = (int)Math.Ceiling(RemainingTime);
        int min = intTime / 60;
        int sec = intTime % 60;
        timer.text = min + ":" + ((sec < 10) ? "0" : "") + sec;

        // Si no ha acabado se sustrae el tiempo entre frames (deltaTime)
        if (!logic.MatchEnded)
            if (intTime > 0) RemainingTime -= Time.deltaTime;
    }
}
