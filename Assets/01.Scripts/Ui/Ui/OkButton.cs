using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkButton : UiButton
{
    protected override void Click()
    {
        GameManager.gameEvent.Call("MenuOff");
    }
}
