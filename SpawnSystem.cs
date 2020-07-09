using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    float time;
    List<Spawn> spawnpoints;
    int numSpawnpoints;

    public GameObject[] models;
    int numModels;

    public GameObject player1;
    public GameObject player2;

    public float securityDistance;
    public float maxDistanceDifference;

    public float fastInterval;
    public float normalInterval;

    int numElements;

    public int minElements;
    public int maxElements;

    public bool randomRotation;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        numElements = 0;
        numModels = models.Length;

        Component[] components = GetComponentsInChildren(typeof(Spawn));
        numSpawnpoints = components.Length;
        
        spawnpoints = new List<Spawn>(numSpawnpoints);
        foreach (Component c in components)
            spawnpoints.Add(c as Spawn);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float interval = numElements < minElements ? fastInterval : normalInterval;
        if(time > interval && numElements < maxElements)
        {
            
            GameObject model = models[Random.Range(0, numModels)];
            Spawn spawn = spawnpoints[Random.Range(0, numSpawnpoints)];
            float d1 = Vector3.Distance(spawn.Position, player1.transform.position);
            float d2 = Vector3.Distance(spawn.Position, player2.transform.position);
            if (spawn.IsEmpty && d1 > securityDistance && d2 > securityDistance && Mathf.Abs(d1 - d2) <= maxDistanceDifference)
            {
                spawn.GenerateChild(model, randomRotation);
                numElements++;
                time = 0;
            }
        }
    }

    public void DeleteOneElement()
    {
        time = 0;
        numElements--;
    }
}
