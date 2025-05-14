using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUiWindow : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On);
        GameManager.gameEvent.Add(Off);
    }

    private void On() => this.gameObject.SetActive(true);
    private void Off() => this.gameObject.SetActive(false);
}
