using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HandleCutscene : MonoBehaviour
{
    public GameObject[] toEnable;
    public GameObject[] toDisable;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.tag == "Player")
        {
            GetComponent<PlayableDirector>().Play();
            TimeHandler.Instance.isMenuActive = true;

            if (!TimeHandler.Instance.time)
            {
                TimeHandler.Instance.timeSwitch();
            }

            foreach(GameObject g in toEnable)
            {
                g.SetActive(true);
            }

            foreach (GameObject g in toDisable)
            {
                g.SetActive(false);
            }
        }
        
    }

}
