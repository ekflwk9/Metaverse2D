using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Serialized serialized;
    [SerializeField] public Image healthBarImg;

    public void MainUiAllToggle()
    {
        bool isUiBar = serialized.uiBar.activeSelf;
        if (isUiBar)
        {
            serialized.uiBar.SetActive(false);
        }
        else
        {
            serialized.uiBar.SetActive(true);
        }
    }

    public void GameStart()
    {
        MainUiAllToggle();
        SceneManager.LoadScene("DungeonStart");
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
        bool isSettings = serialized.settingUi.activeSelf;
        if (isSettings)
            serialized.settingUi.SetActive(false);
        else
            serialized.settingUi.SetActive(true);
    }
}