using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Serialized serialized;
    [SerializeField] public Image healthBarImg;

    [SerializeField] private Sprite videoSprite;
    [SerializeField] private Sprite audioSprite;
    [SerializeField] private Sprite gameSprite;
    [SerializeField] private Sprite videoSelectSprite;
    [SerializeField] private Sprite audioSelectSprite;
    [SerializeField] private Sprite gameSelectSprite;
    [SerializeField] private Image video;
    [SerializeField] private Image audio;
    [SerializeField] private Image game;

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
        bool isSettings = serialized.settingUi.activeSelf;
        if (isSettings)
            serialized.settingUi.SetActive(false);
        else
            serialized.settingUi.SetActive(true);
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