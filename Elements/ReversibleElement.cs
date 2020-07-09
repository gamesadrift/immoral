using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversibleElement : Element
{
    bool activeGood;

    public GameObject good;
    public GameObject bad;
    public int score;

    GameObject created;

    

    // Start is called before the first frame update
    void Start()
    {
        activeGood = Random.Range(0,2) == 1;
        if (activeGood)
            created = Instantiate(good, transform);
        else
            created = Instantiate(bad, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (score == 0)
            Delete(1);
    }

    public override void InteractionBad()
    {
        if (activeGood)
        {
            activeGood = false;
            DeleteCreated();
            created = Instantiate(bad, transform);
            Score(false, score);
            score /= 2;
        }
    }

    public override void InteractionGood()
    {
        if (!activeGood)
        {
            activeGood = true;
            DeleteCreated();
            created = Instantiate(good, transform);
            Score(true, score);
            score /= 2;
        }
    }

    void DeleteCreated()
    {
        created.GetComponent<Animator>().SetTrigger("destroy");
        Destroy(created, 1);
    }
}
