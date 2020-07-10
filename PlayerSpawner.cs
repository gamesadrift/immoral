using Cinemachine;
using Photon.Pun;
using UnityEngine;

// Clase basada en el antiguo PlayerPicker
// pero para multijugador.
public class PlayerSpawner : MonoBehaviour
{
    // Objetos cámara.
    [SerializeField] private GameObject cinemachine = null;
    private CinemachineVirtualCamera camera;

    // Musica Bernie.
    [SerializeField] private GameObject goodMusic = null;

    // Musica Jonas.
    [SerializeField] private GameObject badMusic = null;

    // Objetos y componentes.
    private GameObject GoodGO, BadGO;   // Jugadores.
    private PhotonView GoodPV, BadPV;   // Controlador via Photon.
    private Character GoodC, BadC;      // Personaje.

    private void Start()
    {
        // Estoy jugando a Bernie o a Jonas
        bool isGood = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "good";

        // Conseguimos los objetos y componentes
        camera = cinemachine.GetComponent<CinemachineVirtualCamera>();
        
        GoodGO = GameObject.FindWithTag("GoodGuy");
        GoodPV = GoodGO.GetComponent<PhotonView>();
        GoodC = GoodGO.GetComponent<Character>();
        GoodC.GoodGuy = true;

        BadGO = GameObject.FindWithTag("BadGuy");
        BadPV = BadGO.GetComponent<PhotonView>();
        BadC = BadGO.GetComponent<Character>();
        BadC.GoodGuy = false;

        // Actuamos según quien somos
        if (isGood) iAmGood();
        else iAmBad();
    }

    // Bernie
    private void iAmGood()
    {
        // Musica Bernie
        Instantiate(goodMusic);

        // Conseguimos nuestro controlador (takeover)
        GoodPV.RequestOwnership();

        // La camara debe seguir a Bernie
        camera.Follow = GoodC.transform;
        camera.LookAt = GoodC.transform;

        // Somos el bueno
        GoodC.Player = true; 
    }

    // Jonas
    private void iAmBad()
    {
        // Similar a iAmGood pero con Malo/Jonas
        Instantiate(badMusic);

        BadPV.RequestOwnership();

        camera.Follow = BadC.transform;
        camera.LookAt = BadC.transform;
        BadC.Player = true;
    }

}
