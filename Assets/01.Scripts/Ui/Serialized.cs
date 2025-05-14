using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serialized : MonoBehaviour
{
    // UI On/Off
    [SerializeField] protected GameObject uiBar;
    [SerializeField] protected GameObject settingUi;
    [SerializeField] protected GameObject videoOnOff;
    [SerializeField] protected GameObject audioOnOff;
    [SerializeField] protected GameObject gameOnOff;

    // HP Gauge
    [SerializeField] protected Image healthBarImg;

    // Button Change Sprite
    [SerializeField] protected Sprite videoSprite;
    [SerializeField] protected Sprite audioSprite;
    [SerializeField] protected Sprite gameSprite;
    [SerializeField] protected Sprite videoSelectSprite;
    [SerializeField] protected Sprite audioSelectSprite;
    [SerializeField] protected Sprite gameSelectSprite;
    [SerializeField] protected Image video;
    [SerializeField] protected Image audio;
    [SerializeField] protected Image game;

}