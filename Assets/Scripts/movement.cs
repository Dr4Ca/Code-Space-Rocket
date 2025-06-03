using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    // Komponen
    Rigidbody rb;
    AudioSource audioSource;

    // Parameter
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rotationForce = 10f;
    [SerializeField] AudioClip ThrustSound;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles; 

    private void Start() // Manggil komponen ulang dengan alias
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    } // ^                    ^
//     alias    Komponen yang pengen dipanggil
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
        // Apa yang terjadi kalo thrust di pencet
        if (thrust.IsPressed())
        {
            // Objek bakalan naik ke atas
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
            // Kalo Audio gak ke play, maka play
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(ThrustSound);
            }

            if (!mainEngineParticles.isPlaying)
            {    
                mainEngineParticles.Play();
            }
        }
        else // Pas Thrust udah gak kepencet lagi
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void processRotation()
    { // Ke kanan (-) ke kiri (+)
        float rotationInput = rotation.ReadValue<float>(); // Biar nilai dari rotationInput bisa dilihat
        if (rotationInput < 0) // Kalo nilainya kurang dari 0
        {
            ApplyRotation(rotationForce);
            if (!rightEngineParticles.isPlaying)
            {
                rightEngineParticles.Stop();
                rightEngineParticles.Play();
            }
        }
        else if (rotationInput > 0) // Kalo nilainya lebih dari 0
        {
            ApplyRotation(-rotationForce); // Dikasih (-) karena nilainya berbanding terbalik dgn di atas
            if (!leftEngineParticles.isPlaying)
            {
                leftEngineParticles.Stop();
                leftEngineParticles.Play();
            }
        }
        else
        {
            rightEngineParticles.Stop();
            leftEngineParticles.Stop();
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
