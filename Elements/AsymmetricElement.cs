using UnityEngine;

// Clase para elementos asimétricos:
// -> Dan puntos al que puede activarlos si lo hace.
// -> El otro puede destruirlos por puntos.
public class AsymmetricElement : Element
{
    // Variables sobre puntuaciones que da.
    public float scoreTime;         // Tiempo entre dar punto al activador.
    public float destroyTime;       // Tiempo de invulnerabilidad.
    public int timedScore;          // Puntos cada scoreTime.
    public int destructionScore;    // Puntos al destruir.

    // Objeto para cambio visual al activar.
    public GameObject element;
    // Activador. True = Bernie, False = Jonas.
    public bool activator;

    // Variables para el estado del elemento.
    float time;         // Tiempo que ha pasado activo.
    bool activated;     // Está activo.
    bool destroy;       // Orden de destruir.
    bool destroyed;     // Se ha destruido.

    void Start()
    {
        // Empieza con 0, sin destruir ni activar.
        time = 0;
        destroy = false;
        destroyed = false;
        activated = false;
    }

    void Update()
    {
        // Activado.
        if (activated)
        {
            time += Time.deltaTime;

            // No destruido y toca dar puntos.
            if(!destroy && time >= scoreTime)
            {
                // Reset de tiempo.
                time = 0;
                // Da puntos al activador.
                Score(activator, timedScore);
            }
        }

        // Si se manda a destruir, no esta destruido y no invulnerable.
        if (destroy && !destroyed && time > destroyTime)
        {
            // Da puntos al destructor (el contrario)
            Score(!activator, destructionScore);
            // Se destruye.
            Delete(1);
            destroyed = true;
        }
    }

    // Interacciones de un Element:

    // Bueno.
    public override void InteractionGood()
    {
        // Si puede activarla y estaba dormida, activa.
        if (activator && !activated) Activate();
        // Si puede destruirla y estaba activa, destruye.
        else if (!activator && activated) Destroy();
    }

    // Malo.
    public override void InteractionBad()
    {
        // Al revés en cuanto activador.
        if (!activator && !activated) Activate();
        else if (activator && activated) Destroy();
    }
    
    // Activa el elemento.
    void Activate()
    {
        activated = true;
        // Detalle visual.
        Instantiate(element, transform);
    }

    // Destruye el elemento.
    void Destroy()
    {
        destroy = true;
        time = 0;
    }
}
