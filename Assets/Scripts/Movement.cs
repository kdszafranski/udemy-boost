using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource src;

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
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space)) {
            // particles
            if(!mainThruster.isPlaying) {
                mainThruster.Play();
            }

            rb.AddRelativeForce(Vector3.up * thrustAmount * Time.deltaTime);
            // only play audio if it's not already playing
            if(!src.isPlaying) {
                src.PlayOneShot(thrustSound);
            }
        } else {
            mainThruster.Stop();
            if(src.isPlaying) {
                src.Stop();
            }
        }
    }
    
    void ProcessRotation() {
        float horizontal = Input.GetAxis("Horizontal");

        if(horizontal > 0) {
            // right
            // rightThruster.Play();
            ApplyRotation(-rotationAmount);
        }
        if(horizontal < 0)
        {
            // left
            // leftThruster.Play();
            ApplyRotation(rotationAmount);
        }
    }

    private void ApplyRotation(float direction)
    {
        transform.Rotate(Vector3.forward * direction * Time.deltaTime);
    }
}
