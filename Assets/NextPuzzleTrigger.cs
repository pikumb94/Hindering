using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextPuzzleTrigger : MonoBehaviour
{

    //public SceneAsset nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex);

            //SceneLoader.HandleSceneSwitch(SceneLoader.ScenesFromString( nextScene.name));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
