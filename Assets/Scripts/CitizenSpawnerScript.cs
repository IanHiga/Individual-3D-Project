using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CitizenSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject citizenPrefab;
    [SerializeField] private int minSpawnRate;
    private GameManager gameMgr;
    private int timer;
    private int rate;
    private int milestone;
    private int wait;

    private void Start()
    {
        wait = 0;
        milestone = 1;
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
        rate = minSpawnRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer == 0)
        {
            var rand = Random.Range(0, 2);
            if (rand == 1 || wait == 1)
            {
                // Debug.Log("Spawn new Citizen");
                Instantiate(citizenPrefab, new Vector3(transform.position.x - 5, transform.position.y, transform.position.z), Quaternion.identity);
                if (rate >= 50)
                {
                    rate -= 3;
                }

                wait = 0;
            }
            else
            {
                wait++;
            }
            timer = rate;
        }
        else
        {
            timer--;
        }

        if ((gameMgr.GetPoints() > (milestone * 100))) 
        {
            Debug.Log("MILESTONE");
            if (rate <= minSpawnRate - 50)
            {
                rate += 50;
            }
            else
            {
                rate = minSpawnRate;
            }
            milestone++;
        }
    }
}
