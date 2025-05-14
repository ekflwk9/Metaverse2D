using UnityEngine;

public class UiComponent : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.stopGame = true;
        }
    }

    private void Menu()
    {
        if (!GameManager.stopGame)
        {
            GameManager.gameEvent.Call("MenuOn");
        }

        else
        {
            GameManager.gameEvent.Call("MenuOff");
        }
    }
}
