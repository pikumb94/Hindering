﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{
    [TextArea]
    public String messageToDisplay;
    public float typeSpeed=0.3f;
    public float typeInitialDelay=2;
    private Text textComponent;
    public bool startWithTrigger = false;
    // public int clipsCount;
    // public List<AudioClip> typeClips= new List<AudioClip>();
    private int initialCharPos = 0;



    // Start is called before the first frame update
    void Start()
    {
        if (messageToDisplay == "")
        {
            messageToDisplay = "This is a monitor that aims to display some info for nub players ";
        }
        
        textComponent = GetComponent<Text>();
        StartCoroutine("writeMessage");
      //  clipsCount = typeClips.Count;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator writeMessage()
    {
        yield return new WaitForSeconds(typeInitialDelay);
        while (!TimeHandler.Instance.time)
            yield return null;

        for (int i =0; i< messageToDisplay.Length+1;i++)
        {
            while (!TimeHandler.Instance.time)
                yield return null;
            //QUI LORE TALONE DOVREBBE AGGIUNGERE IL SUONO
            //GetComponent<AudioSource>().PlayOneShot(typeClips[0]);
            textComponent.text = messageToDisplay.Substring(initialCharPos,i-initialCharPos);

            if (CheckTextHeight())
            {
                textComponent.text = "";
                initialCharPos = i;
            }
                
            yield return new WaitForSeconds(typeSpeed);
        }

    }

    protected bool CheckTextHeight()
    {
        float textHeight = LayoutUtility.GetPreferredHeight(textComponent.rectTransform); //This is the height the text would LIKE to be
        float parentHeight = GetComponentInParent<RectTransform>().rect.height; //This is the actual height of the text's parent container

        return (textHeight > parentHeight );
    }
}