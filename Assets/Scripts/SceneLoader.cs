﻿using System;
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
        SampleScene,
        MainMenu,
        Puzzle1,
        Puzzle2
    }
    public  static Scenes ScenesFromString(String nome)
    {
        if (nome=="Puzzle1")
        {
            return Scenes.Puzzle1;
        }
        if (nome == "Puzzle2")
        {
            return Scenes.Puzzle2;
        }
        return Scenes.MainMenu;
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

    internal static void LoadMainMenu()
    {
                SceneManager.LoadScene(Scenes.MainMenu.ToString());

    }

    public static void LoadTargetSceneWithCallback()
    {
        if (targetScene!=null)
        {
            targetScene();
            targetScene = null;
        }
    }
}
