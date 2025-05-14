using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : UiButton
{
    protected override void Click()
    {
        GameManager.gameEvent.Call("TitleOff");
        GameManager.gameEvent.Call("StartUiOff");
        GameManager.gameEvent.Call("MenuOn");

        touchImage.gameObject.SetActive(false);
    }
}
