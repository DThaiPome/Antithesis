using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MMUiManager : MonoBehaviour
{
    public GameObject settings;
    public GameObject MM;
    public AudioMixer AM;

    public Dropdown resolutionDropDowns;

    Resolution[] resolutions;

    public TextMeshProUGUI rebind;

    void Start()
    {
        rebind.text = FirstPersonPlayerObject.kickInput.ToString();
        resolutions = Screen.resolutions;
        resolutionDropDowns.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropDowns.AddOptions(options);
        resolutionDropDowns.value = currentResIndex;
        resolutionDropDowns.RefreshShownValue();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void toSettings()
    {
        settings.SetActive(true);
        MM.SetActive(false);
    }

    public void toMM()
    {
        settings.SetActive(false);
        MM.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        AM.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolution(int resIndex)
    {
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, Screen.fullScreen);
    }
}
