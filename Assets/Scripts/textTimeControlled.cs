using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textTimeControlled : MonoBehaviour
{
    private void Start()
    {
        GameEvents.current.onTimeChange += updateText;
        updateText();
    }
    void updateText()
    {            Text testo = gameObject.GetComponent<Text>();

        if (TimeHandler.Instance.time == true) {
            testo.text = "PLAY MODE";
            testo.color = Color.green;
                }
        else
        {
            testo.text = "STOP MODE";
            testo.color = Color.red;
        }
    }
}
