using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/*
 TimeHandler ha il compito di gestire il tempo. 
 Per aggiungerlo a una scena creare un oggetto vuoto e aggiungere lo script. 
 In seguito biogna aggiungere il componenete "TimeControlled" a tutti gli oggetti che si vogliono manipolare.

 La convenzione è che time uguale a true implica Play mode mentre time uguale a false implica stop Mode
 */
public class TimeHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject commandsMenuPanel;

    [HideInInspector]
    public bool time = false;
    [HideInInspector]
    public bool isMenuActive = false;

    bool inEasterEgg = false;

    private static TimeHandler instance = null;

    // Game Instance Singleton
    public static TimeHandler Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !isMenuActive)
        {
            timeSwitch();
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (Input.GetButtonDown("Start") && SceneManager.GetActiveScene().name!="Ending1")
        {
            if (pauseMenu.activeSelf || commandsMenuPanel.activeSelf)
            {
                pauseMenu.SetActive(false);
                commandsMenuPanel.SetActive(false);
                Time.timeScale = 1f;
                isMenuActive = false;
            }
            else
            {
                
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                isMenuActive = true;
            }

        }
    }


    public void timeSwitch()
    {

        //inverto il tempo
        if (time == true)
        {
            time = false;
        }
        else
        {
            time = true;

        }

        //dico a tutti gli oggetti iscritti all 'evento che il Tempo è cambiato
        GameEvents.current.TimeChange();

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        if (inEasterEgg)
        {
            inEasterEgg = false;
            //togliere il commento in DontDestryOnLoad e appena viene ricaricata la scena Ending2(ovvero inEasterEgg == true) trovare il player e settare la posizione giusta
            //GameObject.Find("Player_RB").transform.position = new Vector3(44, 1, 0);

            
        }

        if (scene.name == "Tutorial1")
            time = true;
        else
            time = false;

        if (scene.name.Contains("Ending2"))
            inEasterEgg = true;

        

    }
}
