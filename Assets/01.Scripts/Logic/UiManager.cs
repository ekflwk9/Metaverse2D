using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    
    [SerializeField] private GameObject startButtonObject;
    [SerializeField] private GameObject exitButtonObject;
    [SerializeField] private GameObject settingButtonObject;
    [SerializeField] private GameObject settingsPanelObject;

    public void MainUiAllToggle()
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
        MainUiAllToggle();
        GameManager.ChangeScene("DungeonStart");
    }

    public void GameExit()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void SettingScreen()
    {
        MainUiAllToggle();
        SettingToggle();
    }

    public void SettingToggle()
    {
        bool isSettings = settingsPanelObject.activeSelf;
        if (isSettings)
            settingsPanelObject.SetActive(false);
        else
            settingsPanelObject.SetActive(true);

        

    }

    
}