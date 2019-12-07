using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMenu : Button
{
    public void click()
    {
        switch(this.name){
          case "StartButton": SceneLoader.HandleSceneSwitch(SceneLoader.Scenes.Level1);
                              break;
          case "LevelsButton": SceneLoader.DirectSceneSwitch(SceneLoader.Scenes.Levels);
                              break;
          case "CommandsButton": SceneLoader.DirectSceneSwitch(SceneLoader.Scenes.Commands);
                              break;
          case "MenuButton": SceneLoader.DirectSceneSwitch(SceneLoader.Scenes.MainMenu);
                              break;
          case "QuitButton":  Application.Quit();
                              break;
          default: SceneLoader.HandleSceneSwitch((SceneLoader.Scenes)System.Enum.Parse( typeof(SceneLoader.Scenes), this.name ));
                    break;
        }
    }
}
