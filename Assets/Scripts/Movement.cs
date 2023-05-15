using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    // editor variables
    [SerializeField] float thrustAmount = 2f;
    [SerializeField] float rotationAmount = 0.2f; // slows down rotation speed
    [SerializeField] AudioClip thrustSound;

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


    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * thrustAmount * Time.deltaTime);
            // only play audio if it's not already playing
            if(!audioSource.isPlaying) {
                audioSource.PlayOneShot(thrustSound);
            }
        } else {
            if(audioSource.isPlaying) {
                audioSource.Stop();
            }
        }
    }
    
    void ProcessRotation() {
         float horizontal = Input.GetAxis("Horizontal");

        if(horizontal > 0) {
            // right
            ApplyRotation(-rotationAmount);
        }
        if(horizontal < 0)
        {
            // left
            ApplyRotation(rotationAmount);
        }
    }

    private void ApplyRotation(float direction)
    {
        transform.Rotate(Vector3.forward * direction * Time.deltaTime);
    }
}
