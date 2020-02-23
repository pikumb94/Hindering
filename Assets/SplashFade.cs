using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;




public class SplashFade : MonoBehaviour
{
    public Image[] images;
    public string sceneName;

    private void Awake()
    {
        foreach (Image img in images)
        {
            img.canvasRenderer.SetAlpha(0.0f);
        }
    }

    IEnumerator Start()
    {
        foreach (Image img in images)
        {
            FadeIn(img);
            yield return new WaitForSeconds(2f);
            FadeOut(img);
            yield return new WaitForSeconds(2f);
        }

        if (sceneName != "")
            //SceneLoader.HandleSceneSwitch((SceneLoader.Scenes)System.Enum.Parse(typeof(SceneLoader.Scenes), sceneName));
            SceneLoader.LoadMainMenu();

        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    void FadeIn(Image img)
    {
        img.CrossFadeAlpha(1.0f,1f,false);
    }

    void FadeOut(Image img)
    {
        img.CrossFadeAlpha(0.0f,2f,false);
    }
}
