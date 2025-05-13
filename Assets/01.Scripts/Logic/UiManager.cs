using UnityEngine;
using UnityEngine.UI;

public class UiManager : Serialized
{
    public void VideoBtn()
    {
        if (video.sprite == videoSprite)
        {
            video.sprite = videoSelectSprite;
            audio.sprite = audioSprite;
            game.sprite = gameSprite;
        }
    }

    public void AudioBtn()
    {
        if (audio.sprite == audioSprite)
        {
            audio.sprite = audioSelectSprite;
            video.sprite = videoSprite;
            game.sprite = gameSprite;
        }
    }

    public void GameBtn()
    {
        if (game.sprite == gameSprite)
        {
            game.sprite = gameSelectSprite;
            video.sprite = videoSprite;
            audio.sprite = audioSprite;
        }
    }


    public void MainUiAllToggle()
    {
        bool isUiBar = uiBar.activeSelf;
        if (isUiBar)
        {
            uiBar.SetActive(false);
        }
        else
        {
            uiBar.SetActive(true);
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

    public void SettingToggle()
    {
        MainUiAllToggle();
        SettingScreen();
    }

    public void SettingScreen()
    {
        bool isSettings = settingUi.activeSelf;
        if (isSettings)
            settingUi.SetActive(false);
        else
            settingUi.SetActive(true);
    }

    public void VideoSettingToggle()
    {

    }

    public void AudioSettingToggle()
    {

    }

    public void GameSettingToggle()
    {

    }
}