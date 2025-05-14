using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : UiButton
{
    protected override void Click() => Application.Quit();
}
