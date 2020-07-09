using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Element
{
    public PotionType type;
    public float duration;

    bool used;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            gameObject.GetComponent<Animator>().SetTrigger("destroy");
            other.gameObject.GetComponent<Character>().StartPotionEffect(type, duration);
            Delete(1);
            used = true;
        }
    }

    public override void InteractionBad()
    {
        //Nothing
    }

    public override void InteractionGood()
    {
        //Nothing
    }
}

public enum PotionType
{
    speed,
    moral
};
