using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject disintegrationEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lightsaber")) // Ensure your lightsaber has the "Lightsaber" tag
        {
            if (!audioSource.isPlaying && audioSource != null)
            {
                audioSource.Play();
            }
            if (disintegrationEffect != null)
            {
                Instantiate(disintegrationEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 0.5f); 
        }
    }
}
