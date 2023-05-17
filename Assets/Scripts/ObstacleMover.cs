using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    Vector3 startingPosition;
    float movementFactor; // length of repeating pattern, larger is slower
    
    [SerializeField] Vector3 movementVector; // location to move to
    [SerializeField] float period = 2f; // length of repeating pattern, larger is slower


    void Start()
    {
        startingPosition = transform.position;    
    }

    void Update()
    {
        if(period != 0) {
            CalculateMovement();
        }
    }

    void CalculateMovement() {
        float cycles = Time.time / period;  // continually growing over time

        const float TAU = Mathf.PI * 2;     // constant 6.283
        float rawSinWave = Mathf.Sin(cycles * TAU); // going from -1 to 1
        
        // recalculated to go from 0 to 1 so its cleaner
        // +1 changes range from 0 - 2 then divide by 2 to get 0 - 1
        movementFactor =  (rawSinWave + 1f) / 2f; 

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset; 
    }
}
