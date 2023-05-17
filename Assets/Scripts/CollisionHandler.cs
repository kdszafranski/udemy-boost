using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelayTime = 2.0f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] Light explosionLight;

    AudioSource audioSource;
    bool allowCollisions = true;
    bool isExploding = false;
    int sceneIndex = 0;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;        
    }
    
    /****************************
    / handle collisions
    /****************************/
    private void OnCollisionEnter(Collision other) {
        if(allowCollisions && !isExploding) {
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
    }

    public void toggleCollisions() {
        allowCollisions = !allowCollisions;
        Debug.Log("Collisions toggled to : " + allowCollisions);
    }

    private void StartSuccessSequence()
    {
        DisableRocket();
        successParticles.Play();
        audioSource.PlayOneShot(successSound);

        // next level
        Invoke("LoadNextLevel", loadDelayTime);
    }

    /****************************
    / stop input and audio then reload the level
    /****************************/
    void StartCrashSequence() {
        DisableRocket();
        isExploding = true;
        
        crashParticles.Play();
        audioSource.PlayOneShot(crashSound);

        MakeExplosionLight();

        // explode select pieces of the rocket
        foreach(Transform child in transform) {
            GameObject go = child.gameObject;
            Debug.Log(go.name);
            if(go.CompareTag("Explodable")) {
                if(go.GetComponent<Collider>() == null) {
                    go.AddComponent<BoxCollider>();
                }
                // now go BOOM!
                go.AddComponent<Rigidbody>();
                go.GetComponent<Rigidbody>().AddForce(transform.position, ForceMode.Impulse);                
            } else {
                // destroy a few parts
                if(!go.CompareTag("DontExplode")) {
                    Destroy(go);
                }
            }
        }

        // start over
        Invoke("ReloadLevel", loadDelayTime);
    }

    void MakeExplosionLight() {
        explosionLight.enabled = true;
        Destroy(explosionLight, .35f);
    }

    /****************************
    / turn off rocket movement and audio
    /****************************/
    private void DisableRocket()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<Movement>().enabled = false;
    }

    /****************************
    / reloads the current scene
    /****************************/
    public void ReloadLevel() {
        SceneManager.LoadScene(sceneIndex);
    }

    /****************************
    / Loads the next scene
    /****************************/
    public void LoadNextLevel() {
        sceneIndex++; // next in build order
        if(sceneIndex >= SceneManager.sceneCountInBuildSettings) {
            sceneIndex = 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

}
