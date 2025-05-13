using UnityEngine;
using UnityEngine.UI;

public class UiManager : Serialized
{
    public void VideoBtn()
    {
        if (video.sprite == videoSprite)
        {
            VideoSetting();
            video.sprite = videoSelectSprite;
            audio.sprite = audioSprite;
            game.sprite = gameSprite;
        }
    }

    public void AudioBtn()
    {
        if (audio.sprite == audioSprite)
        {
            AudioSetting();
            audio.sprite = audioSelectSprite;
            video.sprite = videoSprite;
            game.sprite = gameSprite;
        }
    }

    public void GameBtn()
    {
        if (game.sprite == gameSprite)
        {
            GameSetting();
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

    public void Setting()
    {
        MainUiAllToggle();
        SettingToggle();
        VideoBtn();
    }

    public void SettingToggle()
    {
        bool isSettings = settingUi.activeSelf;
        if (isSettings)
            settingUi.SetActive(false);
        else
            settingUi.SetActive(true);
    }

    public void VideoSetting()
    {
        videoOnOff.SetActive(true);
        audioOnOff.SetActive(false);
        gameOnOff.SetActive(false);
    }

    public void AudioSetting()
    {
        videoOnOff.SetActive(false);
        audioOnOff.SetActive(true);
        gameOnOff.SetActive(false);
    }

    public void GameSetting()
    {
        videoOnOff.SetActive(false);
        audioOnOff.SetActive(false);
        gameOnOff.SetActive(true);
    }
}