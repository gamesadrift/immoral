using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsymmetricElement : Element
{
    float time;
    bool activated;
    bool destroy;
    bool destroyed;

    public GameObject element;
    public bool activator;

    public float scoreTime;
    public float destroyTime;
    public int timedScore;
    public int destructionScore;

    void Start()
    {
        time = 0;
        destroy = false;
        destroyed = false;
        activated = false;
    }

    void Update()
    {
        if (activated)
        {
            time += Time.deltaTime;

            if(!destroy && time >= scoreTime)
            {
                time = 0;
                Score(activator, timedScore);
            }
        }

        if (destroy && !destroyed && time > destroyTime)
        {
            Score(!activator, destructionScore);
            Delete(1);
            destroyed = true;
        }
    }

    public override void InteractionBad()
    {
        if (!activator && !activated) Activate();
        else if (activator && activated) Destroy();
    }

    public override void InteractionGood()
    {
        if (activator && !activated) Activate();
        else if (!activator && activated) Destroy();

    }

    void Activate()
    {
        activated = true;
        Instantiate(element, transform);
    }

    void Destroy()
    {
        destroy = true;
        time = 0;
    }
}
