using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : MonoBehaviour
{
    public float rotationSpeed = 50f; // Continuous rotation speed
    public float swingAngle = 180f; // Max angle of the swing arc
    public float swingDuration = 3f; // Duration of the swing in seconds
    public bool swingOnXAxis = true; // Toggle for swinging on the X or Z axis

    private bool isSwinging = false; // Flag to track if the lightsaber is swinging
    private float swingTimer = 0f; // Timer for the swing animation
    private Quaternion initialRotation; // Starting rotation before swing
    private Quaternion targetRotation; // Target rotation during swing
    public AudioSource audioSource;

    void Start()
    {
        initialRotation = transform.rotation; // Save the initial rotation
    }

    void Update()
    {
        // Handle Enter key press to toggle swing
        if (Input.GetKeyDown(KeyCode.Return) && !isSwinging)
        {
            StartSwing();
        }

        // Continuous rotation around the global y-axis if not swinging
        if (!isSwinging)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }

        // Perform swing if active
        if (isSwinging)
        {
            PerformSwing();
        }
    }

    void StartSwing()
    {
        audioSource.Play();
        // Set swing parameters
        isSwinging = true;
        swingTimer = 0f;
        initialRotation = transform.rotation;

        // Determine the target rotation based on the swing axis
        if (swingOnXAxis)
        {
            targetRotation = Quaternion.Euler(transform.eulerAngles.x + swingAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + swingAngle);
        }
    }

    void PerformSwing()
    {
        swingTimer += Time.deltaTime;

        // Interpolate between initial and target rotation over swing duration
        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, swingTimer / swingDuration);

        // End the swing once the duration is reached
        if (swingTimer >= swingDuration)
        {
            isSwinging = false; // Reset swinging state
            transform.rotation = initialRotation; // Reset to the initial rotation
        }
    }
}
