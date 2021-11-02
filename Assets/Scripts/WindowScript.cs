using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Citizen"))
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
