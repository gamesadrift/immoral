using UnityEngine;

public class OneTimeElement : Element
{
    float time;
    bool activated;
    bool destroyed;

    public GameObject good;
    public GameObject bad;

    public float destroyTime;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        destroyed = false;
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
            time += Time.deltaTime;

        if (!destroyed && time > destroyTime)
        {
            Delete(1);
            destroyed = true;
        }
    }

    public override void InteractionBad()
    {
        if (!activated)
        {
            activated = true;
            Instantiate(bad, transform);
            Score(false, score);
        }
    }

    public override void InteractionGood()
    {
        if (!activated)
        {
            activated = true;
            Instantiate(good, transform);
            Score(true, score);
        }
    }
}
