using UnityEngine;

// Clase para efecto de rotar.
public class Bottle : MonoBehaviour
{
    // Velocidad de rotación.
    public float rotSpeed;

    // Start is called before the first frame update
    void Start() { /* Nada */ }

    // Efecto de rotación.
    void Update() => transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
}
