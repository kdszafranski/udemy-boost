using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    CollisionHandler collisionHandler;

    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) {
            collisionHandler.LoadNextLevel();
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            collisionHandler.toggleCollisions();
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            collisionHandler.ReloadLevel();
        }
    }
}
