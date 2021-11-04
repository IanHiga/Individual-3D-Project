using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text pointDisplay;
    [SerializeField] private string prefix;
    [SerializeField] private RawImage[] errorImages;
    [SerializeField] private GameObject deathScreenObjects;
    private int lives = 3;
    private int points;
    
    public void BouncePoints()
    {
        points += 1;
        pointDisplay.text = prefix + points;
    }

    public void GoalPoints()
    {
        points += 3;
        pointDisplay.text = prefix + points;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    public void LoseCitizen()
    { 
        lives--;
        errorImages[lives].enabled = true;
        var citizens = GameObject.FindGameObjectsWithTag("Citizen");
        float delay = 0.01f;
        foreach (GameObject citizen in citizens)
        {
            delay += 0.01f;
            Destroy(citizen, delay);
        }

        if (lives == 0)
        {
            Time.timeScale = 0;
            deathScreenObjects.SetActive(true);
        }
    }

    public int GetPoints()
    {
        return points;
    }
}
