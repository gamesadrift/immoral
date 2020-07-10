using Photon.Pun;
using UnityEngine;

// Clase genérica para elementos
public abstract class Element : MonoBehaviour
{
    // Posicion de la puntuación.
    [SerializeField] private float scoreUp = 0;
    [SerializeField] private float scoreFront = 0;

    // Sonido al destruir.
    [SerializeField] private AudioSource destroySound = null;

    // Métodos a implementar en los hijos
    // Interacción buena.
    public abstract void InteractionGood();
    //Interacción mala.
    public abstract void InteractionBad();
    
    // Dar puntos.
    public void Score(bool good, int value)
    {
        // Objeto de puntuación.
        Score score = GameObject.Find("Score").GetComponent<Score>();
        Vector3 position = transform.position + new Vector3(0, scoreUp, -scoreFront);

        // Al bueno o al malo.
        if (good) score.GoodPoints(value, position);
        else score.BadPoints(value, position);
    }

    // Se destruye.
    public void Delete(float delay)
    {
        // Sonido.
        if (destroySound != null) destroySound.Play();

        // Animación
        gameObject.GetComponent<Animator>().SetTrigger("destroy");

        // En el MasterClient (quien los crea) se marca como Empty.
        if (PhotonNetwork.IsMasterClient)
            transform.parent.gameObject.GetComponent<Spawn>().MarkAsEmpty();

        // Destruir.
        Destroy(gameObject, delay);
    }

    // Si estan dentro de su collider
    private void OnTriggerStay(Collider other)
    {
        // ¿Qué personaje es?
        Character c = other.gameObject.GetComponent<Character>();

        if (c.Player)
        {
            // Si es el jugador y pulsa espacio...
            if (Input.GetKeyDown("space") || Input.GetKey("space"))
            {
                // hacemos la interacción.
                c.SetInteractionDirection(transform.position);
                if (c.GoodGuy) InteractionGood();
                else InteractionBad();
            }
        }
        else
        {
            // Similar para el otro, pero nos llega por el animator (PhotonView).
            Animator a = other.gameObject.GetComponent<Animator>();
            // Con el flag "interact2".
            if (a.GetBool("interact2"))
            {
                c.SetInteractionDirection(transform.position);
                if (c.GoodGuy) InteractionGood();
                else InteractionBad();
            }
        }
    }
}
