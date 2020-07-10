using UnityEngine;

// Clase para una utilidad del menú principal.
public class MainMenu : MonoBehaviour
{
    // Para boton de QUIT (Salir)
    public void QuitGame()
    {
        // Avisamos por Log (para desarrollo)
        Debug.Log("QUIT!");

        // Salimos de la aplicación
        Application.Quit();
    }
}
