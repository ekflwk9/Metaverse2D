using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject startButtonObject;
    [SerializeField] private GameObject exitButtonObject;
    [SerializeField] private GameObject settingButtonObject;
    [SerializeField] private GameObject settingsPanelObject;

    public void MainUiAllOff()
    {
        bool isStartBtnActive = startButtonObject.activeSelf;
        if (isStartBtnActive)
        {
            startButtonObject.SetActive(false);
            exitButtonObject.SetActive(false);
            settingButtonObject.SetActive(false);
        }
        else
        {
            startButtonObject.SetActive(true);
            exitButtonObject.SetActive(true);
            settingButtonObject.SetActive(true);
        }
    }

    public void GameStart()
    {
        MainUiAllOff();
    }

    public void GameExit()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void SettingOpen()
    {
        MainUiAllOff();
        Settings();
    }

    public void Settings()
    {
        if (settingsPanelObject != null)
        {
            settingsPanelObject.SetActive(true);
        }

    }
}