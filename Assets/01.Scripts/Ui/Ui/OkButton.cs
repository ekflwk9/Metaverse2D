using UnityEngine.SceneManagement;

public class OkButton : UiButton
{
    protected override void Click()
    {
        GameManager.gameEvent.Call("MenuOff");

        if (SceneManager.GetActiveScene().name == "Start")
        {
            GameManager.gameEvent.Call("TitleOn");
            GameManager.gameEvent.Call("StartUiOn");
        }

        else
        {
            GameManager.stopGame = false;
        }

        touchImage.SetActive(false);
    }
}
