using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On, true);
    }

    private void On() => this.gameObject.SetActive(true);
}
