using Photon.Pun;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    [SerializeField] private float scoreUp = 0;
    [SerializeField] private float scoreFront = 0;

    [SerializeField] private AudioSource destroySound = null;

    public abstract void InteractionBad();
    public abstract void InteractionGood();

    public void Score(bool good, int value)
    {
        Score score = GameObject.Find("Score").GetComponent<Score>();
        Vector3 position = transform.position + new Vector3(0, scoreUp, -scoreFront);

        if (good) score.GoodPoints(value, position);
        else score.BadPoints(value, position);
    }

    public void Delete(float delay)
    {
        if (destroySound != null) destroySound.Play();
        gameObject.GetComponent<Animator>().SetTrigger("destroy");
        if (PhotonNetwork.IsMasterClient)
            transform.parent.gameObject.GetComponent<Spawn>().MarkAsEmpty();
        Destroy(gameObject, delay);
    }

    private void OnTriggerStay(Collider other)
    {
        Character c = other.gameObject.GetComponent<Character>();
        if (c.Player)
        {
            if (Input.GetKeyDown("space") || Input.GetKey("space"))
            {
                c.SetInteractionDirection(transform.position);
                if (c.GoodGuy) InteractionGood();
                else InteractionBad();
            }
        }
        else
        {
            Animator a = other.gameObject.GetComponent<Animator>();
            if (a.GetBool("interact2"))
            {
                c.SetInteractionDirection(transform.position);
                if (c.GoodGuy) InteractionGood();
                else InteractionBad();
            }
        }
    }
}
