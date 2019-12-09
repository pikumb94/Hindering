using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour {
	public GameObject[] buttons;


	void Start () {
		foreach (GameObject b in buttons)
    {
       b.GetComponent<Button>().onClick.AddListener(() => b.GetComponent<ButtonMenu>().swapInterface(b.GetComponent<ButtonMenu>().fromInterface, b.GetComponent<ButtonMenu>().toInterface));
    }
	}

}
