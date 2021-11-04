using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CitizenScript : MonoBehaviour
{
    private Transform[] locations;
    private int headTo;
    private float prevBounce;
    private GameManager gameMgr;

    private float maxBounce;
    private int curPlat;
    private int platDiff;
    private float distanceBetween;
    private float finalX;
    private float x;
    // Start is called before the first frame update
    void Start()
    {
        locations = new Transform[6];
        locations[0] = GameObject.FindGameObjectWithTag("0").transform;
        locations[1] = GameObject.FindGameObjectWithTag("1").transform;
        locations[2] = GameObject.FindGameObjectWithTag("2").transform;
        locations[3] = GameObject.FindGameObjectWithTag("3").transform;
        locations[4] = GameObject.FindGameObjectWithTag("4").transform;
        locations[5] = GameObject.FindGameObjectWithTag("Goal").transform;
        // Hard-coding bounces
        maxBounce = 14;
        prevBounce = -12;
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
        headTo = -1;
        x = -12.5f;
        FindNextLocation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (headTo != -1)
        {
            // SPIN
            transform.Rotate(0, 0, -5.0f);
            // Debug.Log(x);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            switch (platDiff)
            {
                case 1:
                    x += 0.02f;
                    break;
                case 2: 
                    x += 0.04f; 
                    break;
                case 3:
                    x += 0.06f;
                    break;
                case 4:
                    x += 0.08f;
                    break;
                case 5:
                    x += 0.1f;
                    break;
                default:
                    x += 0.12f;
                    break;
            }
            transform.position = new Vector3(transform.position.x, CalculateCurve(x), transform.position.z);
            if (transform.position.y <= -3)
            {
                if (headTo == 5)
                {
                    gameMgr.GoalPoints();
                }
                Destroy(this.gameObject);
            }
        }

    }

    private float CalculateCurve(float x)
    {
        distanceBetween = locations[headTo].position.x - prevBounce;
        return ((float)((maxBounce * Math.Sin((Math.PI / distanceBetween) * (x - (prevBounce))))) + 2);
    }
    
    public void FindNextLocation()
    {
        maxBounce -= 1;
        for (int i = 0; i < 5; i++)
        {
            if (headTo == -1 && i == 3)
            {
                prevBounce = -30f - locations[2].position.x;
                x = prevBounce;
                headTo = 2;
                platDiff = 4;
                // Debug.Log("Go to 2");
                return;
            }
            var rand = Random.Range(0, 2);
            if (i > headTo && rand == 1)
            {
                if (headTo != -1)
                {
                    prevBounce = locations[headTo].position.x;
                    x = prevBounce;
                    curPlat = headTo;
                }
                else
                {
                    prevBounce = -30f - locations[i].position.x;
                    x = prevBounce;
                    switch (i)
                    {
                        case 0:
                            curPlat = -2;
                            break;
                        case 1:
                            curPlat = -3;
                            break;
                        case 2:
                            curPlat = 4;
                            break;
                    }
                }
                headTo = i;
                platDiff = headTo - curPlat;
                // Debug.Log("Go to " + i);
                return;
            }
        }
        // No more bounces left, return to goal
        // Debug.Log("Go to " + 5);
        prevBounce = locations[headTo].position.x;
        x = prevBounce;
        curPlat = headTo;
        headTo = 5;
        platDiff = headTo - curPlat;
    }
}
