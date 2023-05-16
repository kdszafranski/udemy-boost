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

    AudioSource audioSource;
    bool allowCollisions = true;
    int sceneIndex = 0;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;        
    }
    
    /****************************
    / handle collisions
    /****************************/
    private void OnCollisionEnter(Collision other) {
        if(allowCollisions) {
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
        crashParticles.Play();        
        
        DisableRocket();
        // Destroy(gameObject, loadDelayTime - 0.3f);

        audioSource.PlayOneShot(crashSound);

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

    public void LoadNextLevel() {
        sceneIndex++; // next in build order
        if(sceneIndex >= SceneManager.sceneCountInBuildSettings) {
            sceneIndex = 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

}
