using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveForce;
    private int padLocation;
    private GameManager gameMgr;

    private void Start()
    {
        padLocation = 2; // 5 locations: [Spawn] 0 | 1 | 2 | 3 | 4 [Goal]
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && padLocation != 0)
        {
            // Move Left 1 location
            padLocation--;
            transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown("d") && padLocation != 4)
        {
            // Move Right 1 location
            padLocation++;
            transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Went through trigger");
        if (other.gameObject.CompareTag("Citizen"))
        {
            other.GetComponent<AudioSource>().Play();
            gameMgr.BouncePoints();
            other.gameObject.GetComponent<CitizenScript>().FindNextLocation();
        }
    }
}
