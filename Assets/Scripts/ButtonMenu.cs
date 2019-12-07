using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenu : Button
{

    public virtual void click()
    {
        Debug.Log("Button clicked");
    }
}
