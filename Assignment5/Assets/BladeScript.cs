using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    private float extendSpeed = 0.1f;
    private bool weaponActive = true; 
    private float scaleMin = 0; 
    private float scaleMax; 
    private float extendDelta; 
    private float scaleCurrent; 
    private float localScaleX, localScaleZ;
    public GameObject blade;
    public AudioSource audioSource;
    public AudioSource powerDown;
    public AudioSource hum;

    void Start()
    {
        localScaleX = transform.localScale.x;
        localScaleZ = transform.localScale.z;

        scaleMax = transform.localScale.y;

        scaleCurrent = scaleMax;

        extendDelta = scaleMax / extendSpeed;

        weaponActive = true;
        
        hum.loop = true;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            extendDelta = weaponActive ? -Mathf.Abs(extendDelta) : Mathf.Abs(extendDelta);
        }

        if (weaponActive && !blade.activeSelf)
        {
            blade.SetActive(true);
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (hum != null && !hum.isPlaying)
            {
                hum.Play();
            }
        } else if (!weaponActive && blade.activeSelf)
        {
            blade.SetActive(false);
            if (powerDown != null && !powerDown.isPlaying)
            {
                powerDown.Play();
            }
            if (hum != null && hum.isPlaying)
            {
                hum.Stop();
            }
        }


        scaleCurrent += extendDelta * Time.deltaTime;
        scaleCurrent = Mathf.Clamp(scaleCurrent, scaleMin, scaleMax);
        transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        weaponActive = scaleCurrent > 0;
    }
}
