using UnityEngine;

public class CardWindow : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On, true);
        GameManager.gameEvent.Add(Off, true);
    }

    private void On()
    {
        GameManager.stopGame = true;
        GameManager.player.StopMove();

        GameManager.gameEvent.Call("Card (0)SetSkill");
        GameManager.gameEvent.Call("Card (1)SetSkill");
        GameManager.gameEvent.Call("Card (2)SetSkill");

        this.gameObject.SetActive(true);
    }

    private void Off()
    {
        GameManager.stopGame = false;
        this.gameObject.SetActive(false);
    }
}
