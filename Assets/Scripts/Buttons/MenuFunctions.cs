using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour {
	public ButtonMenu[] buttons;
	ButtonMenu btn;

	void Start () {
		foreach (ButtonMenu b in buttons)
    {
			 btn =b.GetComponent<ButtonMenu>();
       btn.onClick.AddListener(btn.click);
    }
	}
}
