using UnityEngine;

// Clase para elementos que solo dan puntos una vez
// Al bueno o malo si las activa primero.
public class OneTimeElement : Element
{
    // Variables sobre puntuaciones que da.
    public float destroyTime;  // Tiempo de invulnerabilidad  
    public int score;          // Puntuación.

    // Detalles visuales para interacciones.
    public GameObject good; // Buena.
    public GameObject bad;  // Mala.

    // Varibles para el estado del elemento.
    float time;         // Tiempo activo.
    bool activated;     // Activo.
    bool destroyed;     // Destruido.

    void Start()
    {
        // Empieza con tiempo a cero, sin activar ni destruir.
        time = 0;
        destroyed = false;
        activated = false;
    }

    void Update()
    {
        // Si se activa avanza su tiempo.
        if (activated)
            time += Time.deltaTime;

        // Si no esta destruida y el tiempo supera el destroy, la destruye.
        if (!destroyed && time > destroyTime)
        {
            Delete(1);
            destroyed = true;
        }
    }

    // Interacciones de un Element

    // Bueno
    public override void InteractionGood()
    {
        // Si no está activada.
        if (!activated)
        {
            // Se activa.
            activated = true;
            // Detalle visual.
            Instantiate(good, transform);
            // Da puntos al bueno.
            Score(true, score);
        }
    }

    // Malo
    public override void InteractionBad()
    {
        // Similar pero puntos para el malo.
        if (!activated)
        {
            activated = true;
            Instantiate(bad, transform);
            Score(false, score);
        }
    }
}
