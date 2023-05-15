using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

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
            Debug.Log("We made it!");
            LoadNextLevel();
            break;
        default:
            Debug.Log("Dead!");
            ReloadLevel();
            break;
       }

    }

    /****************************
    / reloads the current scene
    /****************************/
    void ReloadLevel() {
        SceneManager.LoadScene(sceneIndex);
    }

    void LoadNextLevel() {
        sceneIndex++; // next in build order
        if(sceneIndex > SceneManager.sceneCount) {
            sceneIndex = 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

}
