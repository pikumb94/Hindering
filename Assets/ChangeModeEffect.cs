using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class ChangeModeEffect : MonoBehaviour
{
    PostProcessVolume m_Vol;
    PostProcessVolume currVol;
    Vignette m_Vignette;
    ColorGrading m_ColorGrading;

    float intensityValue;
    float temperatureValue;
    float fieldOV;
    public float targetTemperatureVal;
    public float targetIntesityVal;
    public float targetFOV;

    public float transitionTime;
    float vel;
    float velT;
    bool fromStartToPause = true;
    float timer;
    float percTimer;
    public CinemachineVirtualCamera cMCamera;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        /*
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.mode.Override(VignetteMode.Classic);
        m_Vignette.intensity.Override(0);

        m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        m_ColorGrading.enabled.Override(true);
        m_ColorGrading.temperature.Override(0f);
        m_Vol = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette,m_ColorGrading);
        */

        currVol = GetComponent<PostProcessVolume>();
        

        m_Vignette = currVol.profile.GetSetting<Vignette>();
        intensityValue = m_Vignette.intensity.value;

        m_ColorGrading = currVol.profile.GetSetting<ColorGrading>();
        temperatureValue = m_ColorGrading.temperature.value;

        fieldOV = cMCamera.m_Lens.FieldOfView;
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire2")) {
            if (fromStartToPause == true) { 
                fromStartToPause = false;
            } else {
                fromStartToPause = true;
            }
            percTimer = 0f;
            timer = 0f;
        }

        if (percTimer < 1)
        {
            if (fromStartToPause)
            {
                //intensityValue = Mathf.SmoothDamp(intensityValue, targetIntesityVal, ref vel, transitionTime);
                //temperatureValue = Mathf.SmoothDamp(temperatureValue, targetTemperatureVal, ref velT, transitionTime);

                //m_Vignette.intensity.Interp(intensityValue, targetIntesityVal, percTimer);
                m_ColorGrading.temperature.Interp(temperatureValue, targetTemperatureVal, percTimer);
                m_Vignette.intensity.value = Mathf.Sin(timer* 2f * Mathf.PI / (2f * transitionTime)) * targetIntesityVal;
                //Debug.Log(Mathf.Sin(timer * 2f * Mathf.PI / (2f * transitionTime)) * intensityValue);
                //Debug.Log(timer +" " +transitionTime +" "+ intensityValue);
                cMCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(cMCamera.m_Lens.FieldOfView, targetFOV, ref velT, transitionTime);
            }
            else
            {
                /*
                intensityValue = Mathf.SmoothDamp(targetIntesityVal, 0, ref vel, transitionTime);
                temperatureValue = Mathf.SmoothDamp(targetTemperatureVal, 0, ref velT, transitionTime);
                cMCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(cMCamera.m_Lens.FieldOfView, targetFOV, ref velT, transitionTime);

                */
                //m_Vignette.intensity.Interp(targetIntesityVal, intensityValue, percTimer);
                m_ColorGrading.temperature.Interp(targetTemperatureVal, temperatureValue, percTimer);
                cMCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(cMCamera.m_Lens.FieldOfView, fieldOV, ref velT, transitionTime);
            }
            //m_Vignette.intensity.value = intensityValue;
            //m_ColorGrading.temperature.value = temperatureValue;
            percTimer = timer / transitionTime;
            timer += Time.deltaTime;
        }
        
            
        //Debug.Log(timer);
    }

}
