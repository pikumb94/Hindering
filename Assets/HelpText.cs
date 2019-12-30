using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{
    public String messageToDisplay;
    public float typeSpeed=0.3f;
    public float typeInitialDelay=2;
    private Text textComponent;
    public bool startWithTrigger = false;
   // public int clipsCount;
   // public List<AudioClip> typeClips= new List<AudioClip>();
  



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
        
        for (int i =0; i< messageToDisplay.Length+1;i++)
        {
           //QUI LORE TALONE DOVREBBE AGGIUNGERE IL SUONO
           //GetComponent<AudioSource>().PlayOneShot(typeClips[0]);
            textComponent.text = messageToDisplay.Substring(0,i);
            yield return new WaitForSeconds(typeSpeed);
        }

    }
}
