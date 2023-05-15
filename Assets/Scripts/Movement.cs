using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrustAmount = 2f;
    [SerializeField] float rotationAmount = 0.2f; // slows down rotation speed

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
