using System.Security.Cryptography;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    public float scoreUp;
    public float scoreFront;

    public AudioSource destroySound;

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
        transform.parent.gameObject.GetComponent<Spawn>().MarkAsEmpty();
        Destroy(gameObject, delay);
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("space") || Input.GetKey("space"))
        {
            Character c = other.gameObject.GetComponent<Character>();
            c.SetInteractionDirection(transform.position);
            if (c.goodGuy) InteractionGood();
            else InteractionBad();
        }
    }
}
