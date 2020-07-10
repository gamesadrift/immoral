using System.Collections.Generic;
using UnityEngine;

// Clase para elemento progresivo
// Sube de nivel dando mas puntos
// Si llega a un cierto nivel da bastantes desapareciendo
// Puede cambiar de bando por cada interacción
public class ProgressiveElement : Element
{
    // Varaibles para puntuación.
    public float scoreInterval;     // Cada cuanto da puntos.
    public float updateInterval;    // Cada cuanto sube de nivel.

    public int score1;      // Puntos nivel 1.
    public int score2;      // Puntos nivel 2.
    public int score3;      // Puntos nivel 3.
    public int scoreExtra;  // Puntos al desaparecer.

    // Detalles (niveles) visuales.
    public GameObject good1;
    public GameObject good2;
    public GameObject good3;

    public GameObject bad1;
    public GameObject bad2;
    public GameObject bad3;

    List<GameObject> createdObjects;

    // Variables para el estado del elemento.
    float scoreTime;    // Tiempo sin dar puntos.
    float updateTime;   // Tiempo sin subir de nivel.
    int level;          // Nivel.

    void Start()
    {
        scoreTime = 0;
        updateTime = 0;
        level = 0;
        createdObjects = new List<GameObject>(3);
    }

    void Update()
    {
        // Si el nivel no es 0.
        if (level != 0)
        {
            // Sumamos a los tiempos.
            scoreTime += Time.deltaTime;
            updateTime += Time.deltaTime;

            // Si lleva más que el intervalo de dar puntos.
            if (scoreTime >= scoreInterval)
            {
                // Da puntos según el nivel.
                scoreTime = 0;
                switch (level)
                {
                    case 1: Score(true, score1); break;
                    case 2: Score(true, score2); break;
                    case 3: Score(true, score3); break;

                    case -1: Score(false, score1); break;
                    case -2: Score(false, score2); break;
                    case -3: Score(false, score3); break;
                }
            }
            // Si lleva más que el intervalo de subir de nivel.
            if(updateTime >= updateInterval)
            {
                // Sube de nivel.
                updateTime = 0;
                LevelUp();
            }
        }
        // No está activo, nivel es 0.
        else
        {
            scoreTime = 0;
            updateTime = 0;
        }
    }
    // Interacciones de un Element:

    // Bueno.
    public override void InteractionGood()
    {
        // No estaba activo para el bueno.
        if (level <= 0)
        {
            // Se limpia lo visual, extra.
            Clear();
            // Nivel 1 (básico bueno).
            level = 1;
            // Reset de tiempos.
            scoreTime = 0;
            updateTime = 0;
            // Crea detalles visuales del bueno.
            createdObjects.Add(Instantiate(good1, transform));
        }
    }

    // Malo.
    public override void InteractionBad()
    {
        // Similar pero para el malo
        if (level >= 0)
        {
            Clear();
            level = -1;
            scoreTime = 0;
            updateTime = 0;
            createdObjects.Add(Instantiate(bad1, transform));
        }
    }

    // Lógica "Subir de nivel"
    void LevelUp()
    {
        // Para el bueno usa positivos, para el malo negativos.
        switch (level)
        {
            case 1:
                level = 2;
                createdObjects.Add(Instantiate(good2, transform));
                break;
            case 2:
                level = 3;
                createdObjects.Add(Instantiate(good3, transform));
                break;
            case 3:
                Score(true, scoreExtra);
                Delete(1);
                break;
            case -1:
                level = -2;
                createdObjects.Add(Instantiate(bad2, transform));
                break;
            case -2:
                level = -3;
                createdObjects.Add(Instantiate(bad3, transform));
                break;
            case -3:
                Score(false, scoreExtra);
                Delete(1);
                break;
            default:
                break;
        }
    }

    // Limpia detalles visuales.
    void Clear()
    {
        foreach(GameObject obj in createdObjects)
            Destroy(obj);

        createdObjects.Clear();
    }

    
}
