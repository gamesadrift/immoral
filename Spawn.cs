using Photon.Pun;
using UnityEngine;

// Clase para puntos de Spawn (de objetos)
public class Spawn : MonoBehaviour
{
    // Propiedades de la clase.
    public bool IsEmpty { get; private set; }
    public Vector3 Position { get; private set; }

    void Start()
    {
        // Empieza vacío y con la posición asignada.
        IsEmpty = true;
        Position = transform.position;
    }

    // Genera un objeto en ese punto.
    public void GenerateChild(GameObject element, bool randomRotation)
    {
        // Rotación (y rotación aleatoria)
        Quaternion rotation = transform.rotation;
        if (randomRotation)
        {
            float x = Random.Range(-1.0f, 1.0f);
            float z = Random.Range(-1.0f, 1.0f);
            Vector3 forward = new Vector3(x, 0, z);
            rotation = Quaternion.LookRotation(forward, Vector3.up);
        }

        // Instanciamos como objeto de escena.
        GameObject created = PhotonNetwork.InstantiateSceneObject(element.name, transform.position, rotation);

        // Asignarle padre este punto.
        created.transform.parent = transform;
        // No vacio.
        IsEmpty = false;
    }

    // Devolvemos a vacio.
    public void MarkAsEmpty()
    {
        // Está vacío.
        IsEmpty = true;
        transform.parent.gameObject.GetComponent<SpawnSystem>().DeleteOneElement();
    }
}
