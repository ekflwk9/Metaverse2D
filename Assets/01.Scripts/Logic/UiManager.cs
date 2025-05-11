using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Serialized serialized;

    public void MainUiAllToggle()
    {
        bool isStartBtnActive = serialized.startButtonObject.activeSelf;
        if (isStartBtnActive)
        {
            serialized.startButtonObject.SetActive(false);
            serialized.exitButtonObject.SetActive(false);
            serialized.settingButtonObject.SetActive(false);
        }
        else
        {
            serialized.startButtonObject.SetActive(true);
            serialized.exitButtonObject.SetActive(true);
            serialized.settingButtonObject.SetActive(true);
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
        bool isSettings = serialized.settingsPanelObject.activeSelf;
        if (isSettings)
            serialized.settingsPanelObject.SetActive(false);
        else
            serialized.settingsPanelObject.SetActive(true);
    }
}