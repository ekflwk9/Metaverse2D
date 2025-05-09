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
        if (startButtonObject != null)
        {
            startButtonObject.SetActive(false);
        }
        if (exitButtonObject != null)
        {
            exitButtonObject.SetActive(false);
        }
        if (settingButtonObject != null)
        {
            settingButtonObject.SetActive(false);
        }
        if (settingsPanelObject != null)
        {
            settingsPanelObject.SetActive(false);
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
        if (settingsPanelObject != null)
        {
            settingsPanelObject.SetActive(true);
            Debug.Log("설정 창 열림 (관리자 스크립트)");
        }
    }
}