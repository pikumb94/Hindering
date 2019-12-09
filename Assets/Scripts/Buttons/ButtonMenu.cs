using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
  public GameObject fromInterface;
  public GameObject toInterface;

  public void changeSceneLoad(string scene){
    SceneLoader.HandleSceneSwitch((SceneLoader.Scenes)System.Enum.Parse( typeof(SceneLoader.Scenes), scene));
  }
  public void changeSceneDirect(string scene){
    SceneLoader.DirectSceneSwitch((SceneLoader.Scenes)System.Enum.Parse( typeof(SceneLoader.Scenes), scene));
  }
  public void quit(){
    Application.Quit();
  }
  public void swapInterface(GameObject fromInterface, GameObject toInterface){
    fromInterface.SetActive(false);
    toInterface.SetActive(true);
  }
  public void toggleInterface(GameObject fromInterface){
    fromInterface.SetActive(false);
  }
}
