using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour {
	public ButtonMenu start;

	void Start () {
		ButtonMenu btn = start.GetComponent<ButtonMenu>();
		btn.onClick.AddListener(btn.click);
	}
}
