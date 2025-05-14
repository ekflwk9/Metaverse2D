using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : UiButton
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.gameEvent.Call("CardWindowOff");
        GameManager.gameEvent.Call("MenuOff");
    }

    protected override void Click()
    {
        GameManager.fade.OnFade(FadeFunc);
    }

    private void FadeFunc()
    {
        //���� ������ �̵��ؾ���
        GameManager.ChangeScene("DungeonStart");

        GameManager.stopGame = false;
        GameManager.fade.OnFade();
        GameManager.cam.gameObject.SetActive(true);
        GameManager.gameEvent.Call("HpOn");
        GameManager.gameEvent.Call("HpSliderUpdate");
    }
}
