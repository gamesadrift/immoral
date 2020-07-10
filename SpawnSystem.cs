using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] models;

    [SerializeField] private float securityDistance;
    [SerializeField] private float maxDistanceDifference;

    [SerializeField] private float fastInterval;
    [SerializeField] private float normalInterval;

    [SerializeField] private int minElements;
    [SerializeField] private int maxElements;

    [SerializeField] private bool randomRotation;

    [SerializeField] private GameObject GoodGuy = null;
    [SerializeField] private GameObject BadGuy = null;

    private GameLogic logic = null;

    private float time;
    private int numElements;
    private int numModels;

    private int numSpawnpoints;
    private List<Spawn> spawnpoints;

    void Start()
    {
        logic = GameObject.Find("GameLogic").GetComponent<GameLogic>();

        time = 0;
        numElements = 0;
        numModels = models.Length;

        Component[] components = GetComponentsInChildren(typeof(Spawn));
        numSpawnpoints = components.Length;

        spawnpoints = new List<Spawn>(numSpawnpoints);
        foreach (Component c in components)
            spawnpoints.Add(c as Spawn);
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && !logic.MatchEnded)
        {
            time += Time.deltaTime;
            float interval = numElements < minElements ? fastInterval : normalInterval;
            if (time > interval && numElements < maxElements)
            {
                GameObject model = models[Random.Range(0, numModels)];
                Spawn spawn = spawnpoints[Random.Range(0, numSpawnpoints)];
                float d1 = Vector3.Distance(spawn.Position, GoodGuy.transform.position);
                float d2 = Vector3.Distance(spawn.Position, BadGuy.transform.position);
                if (spawn.IsEmpty && d1 > securityDistance && d2 > securityDistance && Mathf.Abs(d1 - d2) <= maxDistanceDifference)
                {
                    spawn.GenerateChild(model, randomRotation);
                    numElements++;
                    time = 0;
                }
            }
        }
    }

    public void DeleteOneElement()
    {
        time = 0;
        numElements--;
    }
}
