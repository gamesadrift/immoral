using Photon.Pun;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator anim;

    [SerializeField] public bool Player { get; set; }
    [SerializeField] public bool GoodGuy { get; set; }

    [SerializeField] private float speedRun;
    [SerializeField] private float speedSprint;
    [SerializeField] private float sensitivity;

    private GameLogic logic;

    private float newH;
    private float newV;
    private Vector3 currentDirection;

    private float interactionTime;
    private Vector3 interactionDirection;

    private float potionTime;
    private float potionDuration;
    private bool potionActive;

    private bool sprint;

    public bool DoubleMoral { get; private set; }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        newH = 0;
        newV = 0;
        potionActive = false;

        logic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }

    void Update()
    {
        if (Player)
        {
            float h = 0;
            float v = 0;

            if (anim.GetBool("interact2"))
            {
                interactionTime += Time.deltaTime;

                if (interactionTime >= 1.2)
                {
                    anim.SetBool("interact2", false);
                    interactionDirection = Vector3.zero;
                }
            }
            else if (Input.GetKeyDown("space") && !logic.MatchEnded)
            {
                interactionTime = 0;
                anim.SetFloat("speed", 0);
                anim.SetBool("interact2", true);
                gameObject.GetComponent<AudioSource>().PlayDelayed(0.35f);
            }
            else if (!logic.MatchEnded)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
            }

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

            if (direction != Vector3.zero)
            {
                currentDirection = Vector3.Slerp(currentDirection, anim.GetBool("interact2") ? interactionDirection : direction, Time.deltaTime * sensitivity);

                transform.rotation = Quaternion.LookRotation(currentDirection);
                if (interactionDirection == Vector3.zero) transform.position += currentDirection * (sprint ? speedSprint : speedRun) * Time.deltaTime;

                anim.SetFloat("speed", directionLength * (sprint ? 2 : 1));
            }
        }
        else
        {
            if (anim.GetBool("interact2"))
            {
                anim.Play("interact");

                //anim.Play("interact 0");
                gameObject.GetComponent<AudioSource>().PlayDelayed(0.35f);
                anim.SetBool("interact2", false);
            }
        }

        if (potionActive)
        {
            potionTime += Time.deltaTime;
            if (potionTime >= potionDuration)
            {
                EndPotionEffects();
            }
        }
    }

    public void SetInteractionDirection(Vector3 position)
    {
        interactionDirection = position - transform.position;
        interactionDirection.y = 0;
    }

    public void StartPotionEffect(PotionType type, float duration)
    {
        EndPotionEffects();

        potionDuration = duration;
        potionTime = 0;
        potionActive = true;

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

    void EndPotionEffects()
    {
        potionActive = false;
        sprint = false;
        DoubleMoral = false;
    }
}
