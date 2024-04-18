using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    CameraCharacter cameraCharacter;

    public float upwardForce = 5f; // Force to move the ghost upwards
    public float downwardSpeed = 10f; // Speed to move the ghost downwards
    public float disableDelay = 2f; // Delay before disabling the ghost

    private bool hasCollided = false; // Flag to prevent multiple collisions

    private void Start()
    {
        cameraCharacter = FindObjectOfType<CameraCharacter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the ghost object and hasn't occurred before
        if (collision.gameObject.CompareTag("Ghost") && !hasCollided)
        {
            hasCollided = true; // Set flag to prevent multiple collisions
            cameraCharacter.hitCount += 1;

            // Apply upward force to the ghost object's Rigidbody
            Rigidbody ghostRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            ghostRigidbody.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);

            // Disable the ghost immediately
            DisableGhost(collision.gameObject);

            Debug.Log("OnCollisionEnterWithBallandGhost");
        }
    }

    private void DisableGhost(GameObject ghost)
    {
        // Move the ghost upwards and disable it after a delay
        ghost.transform.Translate(Vector3.up * upwardForce * Time.deltaTime);
        Invoke("DisableObject", disableDelay);

    }

    private void DisableObject(GameObject obj)
    {
        // Move the ghost downwards immediately
        Rigidbody ghostRigidbody = obj.GetComponent<Rigidbody>();
        ghostRigidbody.velocity = Vector3.down * downwardSpeed;

        // Disable the ghost after a short delay
        Invoke("DisableGhost", disableDelay);
    }

}
