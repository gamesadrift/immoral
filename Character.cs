using UnityEngine;

public class Character : MonoBehaviour {

    Animator anim;

    public bool player;
    public bool goodGuy;

    public float speedRun;
    public float speedSprint;
    public float sensitivity;

    float newH;
    float newV;
    Vector3 currentDirection;

    float interactionTime;
    bool interacting;
    Vector3 interactionDirection;

    float potionTime;
    float potionDuration;
    bool potionActive;

    bool sprint;
    public bool DoubleMoral { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        newH = 0;
        newV = 0;
        potionActive = false;
    }

    void Update()
    {
        if (player)
        {
            float h = 0;
            float v = 0;

            if (interacting)
            {
                interactionTime += Time.deltaTime;

                if (interactionTime >= 1.2)
                {
                    interacting = false;
                    interactionDirection = Vector3.zero;
                }
            }
            else if (Input.GetKeyDown("space"))
            {
                interacting = true;
                interactionTime = 0;
                anim.SetFloat("speed", 0);
                anim.SetTrigger("interact");
                gameObject.GetComponent<AudioSource>().PlayDelayed(0.35f);
            }
            else
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
                currentDirection = Vector3.Slerp(currentDirection, interacting ? interactionDirection : direction, Time.deltaTime * sensitivity);

                transform.rotation = Quaternion.LookRotation(currentDirection);
                if (interactionDirection == Vector3.zero) transform.position += currentDirection * (sprint ? speedSprint : speedRun) * Time.deltaTime;

                anim.SetFloat("speed", directionLength * (sprint ? 2 : 1));
            }
        }
        else
        {
            //CONTROLES DEL OPONENTE (RED)
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
