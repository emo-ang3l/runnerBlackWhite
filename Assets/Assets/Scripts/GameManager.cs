using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject objects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnObjects", 1, 4.2f);
        
    }

    // Update is called once per frame
    void SpawnObjects()
    {
        Instantiate(objects, new Vector3(30f, 0f, 0f), Quaternion.identity);
    }
}
