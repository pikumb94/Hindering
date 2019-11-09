using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
 TimeHandler ha il compito di gestire il tempo. 
 Per aggiungerlo a una scena creare un oggetto vuoto e aggiungere lo script. 
 In seguito biogna aggiungere il componenete "TimeControlled" a tutti gli oggetti che si vogliono manipolare.

 La convenzione è che time uguale a true implica Play mode mentre time uguale a false implica stop Mode
 */
public class TimeHandler : Singleton<TimeHandler>
{
    public bool time = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            timeSwitch();
        }
    }


    void timeSwitch()
    {

        //inverto il tempo
        if (time == true)
        {
            time = false;
        }
        else
        {
            time = true;

        }

        //dico a tutti gli oggetti iscritti all 'evento che il Tempo è cambiato
        GameEvents.current.TimeChange();





    }
}
