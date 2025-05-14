using UnityEngine;

public class MenuWindow : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On, true);
        GameManager.gameEvent.Add(Off, true);

        this.gameObject.SetActive(false);
    }

    private void On()
    {
        GameManager.stopGame = true;
        GameManager.player.StopMove();
        this.gameObject.SetActive(true);
    }

    private void Off()
    {
        GameManager.stopGame = false;
        this.gameObject.SetActive(false);
    }
}
