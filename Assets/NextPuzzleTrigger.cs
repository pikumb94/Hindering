using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPuzzleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ciao");
        if (other.gameObject.tag == "Player")
        {
            SceneLoader.HandleSceneSwitch(SceneLoader.Scenes.Puzzle1);
        }
    }
}
