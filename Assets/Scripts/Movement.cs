using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    // editor variables
    [SerializeField] float thrustAmount = 2f;
    [SerializeField] float rotationAmount = 0.2f; // slows down rotation speed
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] Light thrustLight;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust() 
    {
        if(Input.GetKey(KeyCode.Space)) {
            HandleThrust();
        } else {
            HandleNoThrust();
        }
    }

    void HandleNoThrust()
    {
        mainThruster.Stop(); // stop particles

        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
        if(thrustLight.enabled) {
            thrustLight.enabled = false;
        }
    }

    void HandleThrust()
    {
        // particles
        if (!mainThruster.isPlaying) {
            thrustLight.enabled = true;
            mainThruster.Play();
        }

        rb.AddRelativeForce(Vector3.up * thrustAmount * Time.deltaTime);

        // only play audio if it's not already playing
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(thrustSound);
        }
    }

    void ProcessRotation() 
    {
        float horizontal = Input.GetAxis("Horizontal");

        if(horizontal > 0) {
            RotateRight();
        }

        if (horizontal < 0) {
            RotateLeft();
        }
    }

    void RotateLeft()
    {
        // leftThruster.Play();
        ApplyRotation(rotationAmount);
    }

    void RotateRight()
    {
        // rightThruster.Play();
        ApplyRotation(-rotationAmount);
    }

    void ApplyRotation(float direction)
    {
        transform.Rotate(Vector3.forward * direction * Time.deltaTime);
    }
}
