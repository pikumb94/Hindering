using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PodOpenHandler : MonoBehaviour
{
    Material podGlass;
    public void HandleSignal()
    {
        Debug.Log("IN HANDLE SIGNAL");
        
        SkinnedMeshRenderer mR = GetComponent<SkinnedMeshRenderer>();
        //foreach(Material m in mR.materials)
        //{
         //   if (m.name == "Pod_03_Glass")
         //   {
                Debug.Log(mR.materials[1].name);
                podGlass = mR.materials[1];
                StartCoroutine("FadeGlassPod");
                
           //     break;
          //  }
       // }
        
    }

    public void handleCutsceneFinished(GameObject g)
    {
        g.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator FadeGlassPod()
    {
        Color c;
        for (float ft = 1f; ft >= .25f; ft -= 0.01f)
        {
            c = podGlass.color;
            c.a = ft;
            podGlass.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
