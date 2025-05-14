using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : UiButton
{
    protected override void Click()
    {
        GameManager.fade.OnFade(FadeFunc);
    }

    private void FadeFunc()
    {
        //전투 씬으로 이동해야함
        GameManager.ChangeScene("Test");

        GameManager.stopGame = false;
        GameManager.fade.OnFade();
        GameManager.gameEvent.Call("CameraOn");
        GameManager.gameEvent.Call("HpOn");
    }
}
