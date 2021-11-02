using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScript : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;
    private GameManager gameMgr;
    
    // Start is called before the first frame update
    void Start()
    {
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("LOSE");
        deathSound.Play();
        if (other.gameObject.CompareTag("Citizen"))
        {
            gameMgr.LoseCitizen();
        }
    }
}
