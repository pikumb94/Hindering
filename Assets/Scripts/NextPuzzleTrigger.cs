using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextPuzzleTrigger : MonoBehaviour
{

    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(nextScene != "")
                SceneLoader.HandleSceneSwitch((SceneLoader.Scenes)System.Enum.Parse(typeof(SceneLoader.Scenes), nextScene));

            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
