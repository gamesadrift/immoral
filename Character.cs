using UnityEngine;

// Clase para los personajes.
public class Character : MonoBehaviour
{
    // Componente de este objeto.
    Animator anim;

    // Banderas para otras clases.
    [SerializeField] public bool Player { get; set; }
    [SerializeField] public bool GoodGuy { get; set; }

    // Para movimiento.
    [SerializeField] private float speedRun;
    [SerializeField] private float speedSprint;
    [SerializeField] private float sensitivity;
    private float newH;
    private float newV;
    private Vector3 currentDirection;

    // Para movimiento/interacciones.
    private float interactionTime;
    private Vector3 interactionDirection;

    // Pociones (estado).
    private float potionTime;
    private float potionDuration;
    private bool potionActive;
    // Corriendo (estado).
    private bool sprint;

    // Objeto con la lógica.
    private GameLogic logic;

    // Propiedad que consultan otras clases.
    public bool DoubleMoral { get; private set; }

    void Start()
    {
        // Valores por defecto y componentes.
        anim = gameObject.GetComponent<Animator>();
        newH = 0;
        newV = 0;
        potionActive = false;

        logic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }

    void Update()
    {
        // Si controlamos a este personaje.
        if (Player) 
        {
            // Empezamos con movimiento cero.
            float h = 0;
            float v = 0;

            // Si estamos interactuando hay que esperar a que termine la interacción
            if (anim.GetBool("interact2"))
            {
                interactionTime += Time.deltaTime;

                if (interactionTime >= 1.2)
                {
                    anim.SetBool("interact2", false);
                    interactionDirection = Vector3.zero;
                }
            }

            // Si no, al pulsar espacio empezamos la interacción
            // No se puede si ha acabado la partida.
            else if (Input.GetKeyDown("space") && !logic.MatchEnded)
            {
                interactionTime = 0;
                anim.SetFloat("speed", 0);
                anim.SetBool("interact2", true);
                gameObject.GetComponent<AudioSource>().PlayDelayed(0.35f);
            }

            // Si no, y si tampoco ha acabado la partida, nos movemos segun h y v.
            else if (!logic.MatchEnded)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
            }

            // Calculo del movimiento, rotación, dirección.
            newV = Mathf.Lerp(newV, v, Time.deltaTime * sensitivity);
            newH = Mathf.Lerp(newH, h, Time.deltaTime * sensitivity);

            if (Mathf.Abs(newV) < 1e-4) newV = 0;
            else if (newV > 1 - 1e-4) newV = 1;
            else if (newV < -1 + 1e-4) newV = -1;

            if (Mathf.Abs(newH) < 1e-4) newH = 0;
            else if (newH > 1 - 1e-4) newH = 1;
            else if (newH < -1 + 1e-4) newH = -1;

            Vector3 direction = new Vector3(newH, 0, newV);

            float directionLength = Mathf.Min(direction.magnitude, 1);
            direction = direction.normalized * directionLength;

            // Si no es cero se mueve.
            if (direction != Vector3.zero)
            {
                currentDirection = Vector3.Slerp(currentDirection, anim.GetBool("interact2") ? interactionDirection : direction, Time.deltaTime * sensitivity);

                transform.rotation = Quaternion.LookRotation(currentDirection);
                if (interactionDirection == Vector3.zero) transform.position += currentDirection * (sprint ? speedSprint : speedRun) * Time.deltaTime;

                // Pasamos la velocidad al Animator
                anim.SetFloat("speed", directionLength * (sprint ? 2 : 1));
            }
        }

        // Si no lo controlamos.
        else
        {
            // Si por Photon se activo el bool "interact2"
            if (anim.GetBool("interact2"))
            {
                // Debe hacer la animación y el sonido.
                anim.Play("interact");
                gameObject.GetComponent<AudioSource>().PlayDelayed(0.35f);
                // Lo devolvemos a false.
                anim.SetBool("interact2", false);
            }
        }

        // Vamos reduciendo el tiempo de poción.
        if (potionActive)
        {
            potionTime += Time.deltaTime;
            if (potionTime >= potionDuration)
                EndPotionEffects(); // Se acabó.
        }
    }

    // Para interactuar en una dirección con el objeto.
    public void SetInteractionDirection(Vector3 position)
    {
        interactionDirection = position - transform.position;
        interactionDirection.y = 0;
    }

    // Damos el efecto de la poción.
    public void StartPotionEffect(PotionType type, float duration)
    {
        EndPotionEffects(); // Quitamos todas (solo una activa).

        // Variables sobre duración.
        potionDuration = duration;
        potionTime = 0;
        potionActive = true;

        // Activamos efecto correspondiente.
        switch (type)
        {
            case PotionType.moral:
                DoubleMoral = true;
                break;
            case PotionType.speed:
                sprint = true;
                break;
        }
    }

    // Quitar efectos de cualquier poción.
    void EndPotionEffects()
    {
        potionActive = false;
        sprint = false;
        DoubleMoral = false;
    }
}
