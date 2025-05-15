using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private void End()
    {
        GameManager.ChangeScene("Start");

        GameManager.player.StateUp(StateCode.Health, 100);
        GameManager.gameEvent.Call("HpSliderUpdate");
        GameManager.gameEvent.Call("HpOff");
        GameManager.cam.gameObject.SetActive(false);
    }
}
