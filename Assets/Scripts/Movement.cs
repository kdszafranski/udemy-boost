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
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem leftThruster;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
    }

    void HandleThrust()
    {
        // particles
        if (!mainThruster.isPlaying) {
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
