using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelayTime = 2.0f;
    int sceneIndex = 0;

    private void Start() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;        
    }
    
    /****************************
    / handle collisions
    /****************************/
    private void OnCollisionEnter(Collision other) {
       switch(other.gameObject.tag) {
        case "Friendly":
            Debug.Log("friendly collision");
            break;
        case "Goal":
            StartSuccessSequence();
            break;
        default:
            StartCrashSequence();
            break;
       }

    }

    private void StartSuccessSequence()
    {
        DisableRocket();
        // next level
        Invoke("LoadNextLevel", loadDelayTime);
    }

    /****************************
    / stop input and audio then reload the level
    /****************************/
    void StartCrashSequence() {
        DisableRocket();      
        // start over
        Invoke("ReloadLevel", loadDelayTime);
    }

    /****************************
    / turn off rocket movement and audio
    /****************************/
    private void DisableRocket()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Movement>().enabled = false;
    }

    /****************************
    / reloads the current scene
    /****************************/
    void ReloadLevel() {
        SceneManager.LoadScene(sceneIndex);
    }

    void LoadNextLevel() {
        sceneIndex++; // next in build order
        if(sceneIndex >= SceneManager.sceneCountInBuildSettings) {
            sceneIndex = 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

}
