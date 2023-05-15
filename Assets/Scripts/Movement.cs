using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
