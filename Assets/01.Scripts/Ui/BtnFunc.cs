using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnFunc : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image image;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite clickImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (image != null && clickImage != null)
        {
            image.sprite = clickImage;

        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (image != null && defaultImage != null)
        {
            image.sprite = defaultImage;
        }
    }

    public void GameStart()
    {
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
        SettingOpen();
    }

    public void SettingOpen()
    {
        Transform parentTransform = transform.parent;
        Transform settingsTransform = parentTransform.Find("Settings");

        settingsTransform.gameObject.SetActive(true);
    }
}