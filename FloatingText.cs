using UnityEngine;

// Texto flotante para puntuación.
public class FloatingText : MonoBehaviour
{
    // Tiempo que tarda en destruirse.
    public float destroyTime;

    // Colores.
    public Color goodColor;
    public Color badColor;

    // Sonidos.
    public AudioSource goodSound;
    public AudioSource badSound;

    // Se autodestruye al pasar el tiempo.
    void Start() => Destroy(gameObject, destroyTime);
}
