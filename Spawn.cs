﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool IsEmpty { get; private set; }
    public Vector3 Position { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        IsEmpty = true;
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateChild(Object element, bool randomRotation)
    {
        Quaternion rotation = transform.rotation;
        if (randomRotation)
        {
            float x = Random.Range(-1.0f, 1.0f);
            float z = Random.Range(-1.0f, 1.0f);
            Vector3 forward = new Vector3(x, 0, z);
            rotation = Quaternion.LookRotation(forward, Vector3.up);
        }

        Instantiate(element, transform.position, rotation, transform);
        IsEmpty = false;
    }

    public void MarkAsEmpty()
    {
        IsEmpty = true;
        transform.parent.gameObject.GetComponent<SpawnSystem>().DeleteOneElement();
    }
}