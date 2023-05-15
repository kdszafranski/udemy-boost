using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrustAmount = 2f;

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
            // thrust
            Debug.Log("thrusting");
            rb.AddRelativeForce(Vector3.up * thrustAmount * Time.deltaTime);
        }       
    }
    
    void ProcessRotation() {
         float horizontal = Input.GetAxis("Horizontal");

        if(horizontal > 0) {
            // right
            Debug.Log("right");
        }
        if(horizontal < 0) {
            Debug.Log("left");
        }
    }
}
