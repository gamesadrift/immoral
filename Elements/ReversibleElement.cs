using UnityEngine;

// Clase para objeto reversible.
// -> Aparece en un estado (versión).
// -> Da puntos (cada vez menos) cada vez que cambia.
// -> Al dar el minimo (1) desaparece.
public class ReversibleElement : Element
{
    // Variable para el estado del elemento.
    [SerializeField] bool activeGood; // ¿Empieza en la versión buena?

    // Detalles visuales
    public GameObject good;     // Versión buena.
    public GameObject bad;      // Versión mala.
    GameObject created;

    // Variable sobre puntuación.
    public int score;

    void Start()
    {
        // Si empieza para el malo instanciamos la buena...
        if (activeGood) created = Instantiate(good, transform);
        // y viceversa.
        else created = Instantiate(bad, transform);
    }
   
    void Update()
    {
        // Si va a dar 0 puntos se destruye.
        if (score == 0)  Delete(1);
    }

    // Interacciones de un Element:

    // Bueno.
    public override void InteractionGood()
    {
        // Estaba activa la versión mala.
        if (!activeGood)
        {
            // Activamos la buena.
            activeGood = true;
            // Borramos la mala.
            DeleteCreated();
            // Creamos la buena.
            created = Instantiate(good, transform);
            // Sumamos al bueno.
            Score(true, score);
            // Reducimos la puntuación del siguiente.
            score /= 2;
        }
    }

    // Malo.
    public override void InteractionBad()
    {
        // Similar pero para el malo.
        if (activeGood)
        {
            activeGood = false;
            DeleteCreated();
            created = Instantiate(bad, transform);
            Score(false, score);
            score /= 2;
        }
    }

    // Borra la versión actual, con animación.
    void DeleteCreated()
    {
        created.GetComponent<Animator>().SetTrigger("destroy");
        Destroy(created, 1);
    }
}
