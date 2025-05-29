using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rotationForce = 10f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    void FixedUpdate()
    {
        processThrust();
        processRotation();
    }

    private void processThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void processRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationForce);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationForce);
        }
    }

    private void ApplyRotation(float rotationFrame)
    {
        rb.freezeRotation = true; 
        // Rotate the object around its forward axis    
        transform.Rotate(Vector3.forward * rotationFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; 
    }
}
