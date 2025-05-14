using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (GameManager.player.gameObject.activeSelf) Menu();
        }
    }

    private void Menu()
    {
        if (SceneManager.GetActiveScene().name != "Start")
        {
            if (!GameManager.stopGame)
            {
                GameManager.stopGame = true;
                GameManager.player.StopMove();
                GameManager.gameEvent.Call("MenuOn");
            }

            else
            {
                GameManager.stopGame = false;
                GameManager.gameEvent.Call("MenuOff");
            }
        }
    }
}
