using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

using UnityEngine.SceneManagement;

public class ChangeModeEffect : MonoBehaviour
{
    PostProcessVolume m_Vol;
    PostProcessVolume currVol;
    Vignette m_Vignette;
    ColorGrading m_ColorGrading;
    ChromaticAberration m_ChromaticAberration;
    LensDistortion m_LensDistortion;

    AutoExposure m_AutoExposure;
    Grain m_Grain;

    float intensityValue;
    float temperatureValue;
    float fieldOV;
    float chromaticAberration;
    float lensDistortion;

    float grain;

    public float targetTemperatureVal;
    public float targetIntesityVal;
    public float targetFOV;
    public float targetAberration;

    public float targetDistorsionS2P;
    public float targetDistorsionP2S;

    public float transitionTime;
    float vel;
    float velT;
    bool fromStartToPause = true;
    float timer;
    float percTimer;
    public CinemachineVirtualCamera cMCamera;

    bool oneShot = false;
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

        m_ChromaticAberration = currVol.profile.GetSetting<ChromaticAberration>();
        chromaticAberration = m_ChromaticAberration.intensity.value;

        m_LensDistortion = currVol.profile.GetSetting<LensDistortion>();
        lensDistortion = m_LensDistortion.intensity.value;

        fieldOV = cMCamera.m_Lens.FieldOfView;

        m_Grain = currVol.profile.GetSetting<Grain>();
        grain = m_Grain.intensity.value;
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire2") && !TimeHandler.Instance.isMenuActive) {

            percTimer = 0f;
            timer = 0f;
        }

        if (percTimer < 1)
        {
            if (!TimeHandler.Instance.time)
            {

                m_ChromaticAberration.intensity.Interp(chromaticAberration, targetAberration,percTimer);
                m_ColorGrading.temperature.Interp(temperatureValue, targetTemperatureVal, percTimer);
                m_Vignette.intensity.value = Mathf.Sin(timer* 2f * Mathf.PI / (2f * transitionTime)) * targetIntesityVal;
                m_LensDistortion.intensity.value = Mathf.Sin(timer * 2f * Mathf.PI / (2f * transitionTime)) * targetDistorsionS2P;

                cMCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(cMCamera.m_Lens.FieldOfView, targetFOV, ref velT, transitionTime);
            }
            else
            {
                m_ChromaticAberration.intensity.Interp(targetAberration , chromaticAberration, percTimer);
                m_ColorGrading.temperature.Interp(targetTemperatureVal, temperatureValue, percTimer);
                m_Vignette.intensity.value = Mathf.Sin(timer * 2f * Mathf.PI / (2f * transitionTime)) * targetIntesityVal;

                m_LensDistortion.intensity.value = Mathf.Sin(timer * 2f * Mathf.PI / (2f * transitionTime)) * targetDistorsionP2S;
                cMCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(cMCamera.m_Lens.FieldOfView, fieldOV, ref velT, transitionTime);
            }

            percTimer = timer / transitionTime;
            timer += Time.deltaTime;
        }



        if (Input.GetButtonDown("RestartScene") && !TimeHandler.Instance.isMenuActive)
        {
            
            if (!oneShot) { 
                StartCoroutine("restartPP");
                oneShot = true;
            }
        }

    }

    IEnumerator restartPP()
    {
        float totalAnimTime =1f;
        float percAnim=0f;

        for (float ft = 0f; ft <= totalAnimTime; ft = ft+ .01f)
        {
            percAnim = ft / totalAnimTime;
            m_Grain.intensity.Interp(grain, 1f, percAnim);
            m_LensDistortion.intensity.value = Mathf.Lerp(lensDistortion,-100f,percAnim);
            m_ColorGrading.postExposure.Interp(0f, -5f, percAnim);
            yield return new WaitForSeconds(.01f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
