using UnityEngine;

// Efecto de audio por codigo para que la musica del menu no sea siempre "buena".
public class MenuAudioEffect : MonoBehaviour
{
    // Tiempos para el efecto.
    private static int countDown_0 = 3200;      // Tiempo en frames de la música.
    private static int countDown2_0 = 400;      // Tiempo en frames del efecto.

    // Variables derivadas.
    private int countDown = countDown_0;
    private int countDown2 = countDown2_0;
    private int half_cd2 = countDown2_0 / 2;
    private int quarter_cd2 = countDown2_0 / 4;

    void Update()
    {
        // Componente con la musica.
        AudioSource AS = this.gameObject.GetComponent<AudioSource>();

        // Esta sonando la buena.
        if (countDown != 0) countDown -= 1;
        // Cuando se acaba hace el efecto (baja el pitch).
        else
        {
            // Efecto.
            float first_term = (float)countDown2 / (float)quarter_cd2;
            if (countDown2 > half_cd2) AS.pitch = first_term - 3.0f;
            else AS.pitch = -first_term + 1.0f;
 
            // Mientras dura el efecto.
            if (countDown2 != 0) countDown2 -= 1;
            // Devuelve a la normalidad.
            else
            {
                countDown = countDown_0;
                countDown2 = countDown2_0;
                AS.pitch = 1;
            }
        }
    }
}
