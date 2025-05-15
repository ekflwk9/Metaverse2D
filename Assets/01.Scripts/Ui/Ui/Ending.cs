using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private void End()
    {
        GameManager.ChangeScene("Start");
        GameManager.stopGame = false;
    }
}
