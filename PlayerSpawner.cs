using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cinemachine = null;
    private CinemachineVirtualCamera camera;

    // Good
    [SerializeField] private GameObject goodPrefab = null;
    [SerializeField] private GameObject goodSpawn = null;
    [SerializeField] private GameObject goodMusic = null;

    // Bad
    [SerializeField] private GameObject badPrefab = null;
    [SerializeField] private GameObject badSpawn = null;
    [SerializeField] private GameObject badMusic = null;

    private GameObject GoodGO, BadGO;
    private PhotonView GoodPV, BadPV;
    private Character GoodC, BadC;

    private void Start()
    {
        camera = cinemachine.GetComponent<CinemachineVirtualCamera>();
        bool isGood = (string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "good";

        GoodGO = GameObject.FindWithTag("GoodGuy");
        GoodPV = GoodGO.GetComponent<PhotonView>();
        GoodC = GoodGO.GetComponent<Character>();
        GoodC.GoodGuy = true;

        BadGO = GameObject.FindWithTag("BadGuy");
        BadPV = BadGO.GetComponent<PhotonView>();
        BadC = BadGO.GetComponent<Character>();
        BadC.GoodGuy = false;

        if (isGood) iAmGood();
        else iAmBad();
    }

    private void iAmGood()
    {
        Instantiate(goodMusic);

        GoodPV.RequestOwnership();

        camera.Follow = GoodC.transform;
        camera.LookAt = GoodC.transform;
        GoodC.Player = true; 
    }

    private void iAmBad()
    {
        Instantiate(badMusic);

        BadPV.RequestOwnership();

        camera.Follow = BadC.transform;
        camera.LookAt = BadC.transform;
        BadC.Player = true;
    }

}
