using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Systema de Spawn de objetos
public class SpawnSystem : MonoBehaviour
{
    // Modelos disponibles en escenario.
    [SerializeField] private GameObject[] models;

    // Distancias para la fórmula.
    [SerializeField] private float securityDistance;
    [SerializeField] private float maxDistanceDifference;

    // Intervalos de tiempo.
    [SerializeField] private float fastInterval;
    [SerializeField] private float normalInterval;

    // Máximo y mínimo de elementos.
    [SerializeField] private int minElements;
    [SerializeField] private int maxElements;

    // ¿Rotación aleatoria en este escenario?
    [SerializeField] private bool randomRotation;

    // Bueno y malo
    [SerializeField] private GameObject GoodGuy = null;
    [SerializeField] private GameObject BadGuy = null;

    // Objeto con la lógica
    private GameLogic logic = null;

    // Variables auxiliares
    private float time;
    private int numElements;
    private int numModels;

    private int numSpawnpoints;
    private List<Spawn> spawnpoints;

    void Start()
    {
        // Buscar componentes e inicializar todo.
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
        // Si la partida no ha acabado, el MasterClient genera objetos.
        if (PhotonNetwork.IsMasterClient && !logic.MatchEnded)
        {
            // Fórmula de Spawn, tiene en cuenta tiempo entre generaciones
            // distancia a ambos jugadores y si está vacío el punto donde intenta.
            time += Time.deltaTime;
            float interval = numElements < minElements ? fastInterval : normalInterval;
            if (time > interval && numElements < maxElements)
            {
                // Modelo y punto escogidos al azar
                GameObject model = models[Random.Range(0, numModels)];
                Spawn spawn = spawnpoints[Random.Range(0, numSpawnpoints)];

                // Distancias
                float d1 = Vector3.Distance(spawn.Position, GoodGuy.transform.position);
                float d2 = Vector3.Distance(spawn.Position, BadGuy.transform.position);

                // Si se cumplen las condiciones lo genera.
                if (spawn.IsEmpty && d1 > securityDistance && d2 > securityDistance && Mathf.Abs(d1 - d2) <= maxDistanceDifference)
                {
                    spawn.GenerateChild(model, randomRotation);
                    numElements++;
                    time = 0;
                }
            }
        }
    }

    // Al eliminar un elemento.
    public void DeleteOneElement()
    {
        // Tiempo vuelve a 0.
        time = 0;
        // Un elemento menos.
        numElements--;
    }
}
