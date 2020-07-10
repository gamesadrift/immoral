using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicker : MonoBehaviour
{
    public bool goodGuy;
    public GameObject cinemachine;

    public GameObject musicGood;
    public GameObject musicBad;

    public Character good;
    public Character bad;

    private CinemachineVirtualCamera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = cinemachine.GetComponent<CinemachineVirtualCamera>();
        good = GameObject.Find("GoodGuy").GetComponent<Character>();
        bad = GameObject.Find("BadGuy").GetComponent<Character>();

        if (goodGuy)
        {
            Instantiate(musicGood);
            camera.Follow = good.transform;
            camera.LookAt = good.transform;
            good.Player = true;
        }
        else
        {
            Instantiate(musicBad);
            camera.Follow = bad.transform;
            camera.LookAt = bad.transform;
            bad.Player = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
