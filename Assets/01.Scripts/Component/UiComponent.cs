using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiComponent : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);
    }
}
