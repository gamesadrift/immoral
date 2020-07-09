using System.Collections.Generic;
using UnityEngine;

public class ProgressiveElement : Element
{
    float scoreTime;
    float updateTime;
    int level;

    public GameObject good1;
    public GameObject good2;
    public GameObject good3;

    public GameObject bad1;
    public GameObject bad2;
    public GameObject bad3;

    List<GameObject> createdObjects;

    public float scoreInterval;
    public float updateInterval;

    public int score1;
    public int score2;
    public int score3;
    public int scoreExtra;
    

    // Start is called before the first frame update
    void Start()
    {
        scoreTime = 0;
        updateTime = 0;
        level = 0;
        createdObjects = new List<GameObject>(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (level != 0)
        {
            scoreTime += Time.deltaTime;
            updateTime += Time.deltaTime;

            if (scoreTime >= scoreInterval)
            {
                scoreTime = 0;
                switch (level)
                {
                    case 1: Score(true, score1); break;
                    case 2: Score(true, score2); break;
                    case 3: Score(true, score3); break;

                    case -1: Score(false, score1); break;
                    case -2: Score(false, score2); break;
                    case -3: Score(false, score3); break;
                }
            }
            if(updateTime >= updateInterval)
            {
                updateTime = 0;
                LevelUp();
            }
        }
        else
        {
            scoreTime = 0;
            updateTime = 0;
        }
    }

    public override void InteractionBad()
    {
        if (level >= 0)
        {
            Clear();
            level = -1;
            scoreTime = 0;
            updateTime = 0;
            createdObjects.Add(Instantiate(bad1, transform));
        }
    }

    public override void InteractionGood()
    {
        if (level <= 0)
        {
            Clear();
            level = 1;
            scoreTime = 0;
            updateTime = 0;
            createdObjects.Add(Instantiate(good1, transform));
        }
    }

    void LevelUp()
    {
        switch (level)
        {
            case 1:
                level = 2;
                createdObjects.Add(Instantiate(good2, transform));
                break;
            case 2:
                level = 3;
                createdObjects.Add(Instantiate(good3, transform));
                break;
            case 3:
                Score(true, scoreExtra);
                Delete(1);
                break;
            case -1:
                level = -2;
                createdObjects.Add(Instantiate(bad2, transform));
                break;
            case -2:
                level = -3;
                createdObjects.Add(Instantiate(bad3, transform));
                break;
            case -3:
                Score(false, scoreExtra);
                Delete(1);
                break;
            default:
                break;
        }
    }

    void Clear()
    {
        foreach(GameObject obj in createdObjects)
            Destroy(obj);

        createdObjects.Clear();
    }

    
}
