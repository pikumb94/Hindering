using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    private static Action targetScene;
    public enum Scenes
    {
        //examples
        intro,
        tutorial,
        loading,
        firstScene,
    }
    // Start is called before the first frame update
    public static void HandleSceneSwitch(Scenes scene)
    {   //l'argomento 'scena' è la scena target in cui vogliamo andare
        //prima di tutto viene settata la variabile 'targetScene' a una FUNZIONE (senza eseguirla)che carica effettivamente la scena target
        targetScene = () => {
            SceneManager.LoadScene(scene.ToString());
        };

        //poi viene chiamata la scena 'loading' che mostra la schermata di caricamento
        //NB: appena la scena 'loading' caricata la funzione 'targetScene' viene chiamata e si passa alla scena target
        //questo per far eseguire almeno un frame nella scena di caricamento e mostrarla a scermo MENTRE la scena target viene caricata in background
        SceneManager.LoadScene(Scenes.loading.ToString());
    }
    public static void LoadTargetScene()
    {
        if (targetScene!=null)
        {
            targetScene();
            targetScene = null;
        }
    }
}
