using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiComponent : MonoBehaviour
{
    [SerializeField] private GameObject MainUi;

    private void Awake()
    {
        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);

        bool isMainUiTrue = MainUi.activeSelf;
        if (!isMainUiTrue)
        {
            MainUi.SetActive(true);
        }
    }
}
