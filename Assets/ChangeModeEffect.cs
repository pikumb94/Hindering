using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class ChangeModeEffect : MonoBehaviour
{
    PostProcessVolume m_Vol;
    Vignette m_Vignette;
    ColorGrading m_ColorGrading;
    float intensityValue;
    float temperatureValue;
    public float targetTemperatureVal;
    public float targetIntesityVal;
    public float transitionTime;
    public float targetFOV;
    float vel;
    float velT;
    bool fromStartToPause = false;
    float timer;
    public CinemachineVirtualCamera cMCamera;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.mode.Override(VignetteMode.Classic);
        m_Vignette.intensity.Override(.5f);

        m_ColorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        m_ColorGrading.enabled.Override(true);
        m_ColorGrading.temperature.Override(0f);
        m_Vol = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette,m_ColorGrading);
        
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.T)){
            //if(timer>=transitionTime)
                fromStartToPause = true;
        }
        
        if(fromStartToPause)
        {
            intensityValue = Mathf.SmoothDamp(intensityValue, targetIntesityVal, ref vel, transitionTime);
            temperatureValue = Mathf.SmoothDamp(temperatureValue, targetTemperatureVal, ref velT, transitionTime);
            cMCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(cMCamera.m_Lens.FieldOfView, targetFOV, ref velT, transitionTime);
            /*}
            //else
            //{
                intensityValue = Mathf.SmoothDamp(targetIntesityVal, 0, ref vel, transitionTime);
                temperatureValue = Mathf.SmoothDamp(targetTemperatureVal, 0, ref velT, transitionTime);*/
        }
        Debug.Log(fromStartToPause + " Int Val: " + intensityValue+ " Temp Val: "+ temperatureValue);
        m_Vignette.intensity.value = intensityValue;
        m_ColorGrading.temperature.value = temperatureValue;
        
        timer += Time.deltaTime;
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Vol, true, true);
    }
}
