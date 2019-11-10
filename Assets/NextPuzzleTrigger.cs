using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextPuzzleTrigger : MonoBehaviour
{

    public SceneAsset nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneLoader.HandleSceneSwitch(SceneLoader.ScenesFromString( nextScene.name));
        }
    }
}
