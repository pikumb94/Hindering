using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunctions :MonoBehaviour
{
    public Button start;
    void Start()
    {
        start.onClick.AddListener(()=>startGame());
    }
    public void startGame()
    {
        Debug.Log("");
       // SceneLoader.HandleSceneSwitch(SceneLoader.Scenes.Puzzle1);

    }
}
