using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : UiButton
{
    protected override void Click()
    {
        button.text = "TEMP";
    }
}
