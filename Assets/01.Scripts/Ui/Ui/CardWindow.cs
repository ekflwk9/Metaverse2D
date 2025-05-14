using UnityEngine;

public class CardWindow : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On, true);
        GameManager.gameEvent.Add(Off, true);
        this.gameObject.SetActive(false);
    }

    private void On()
    {
        GameManager.player.StopMove();
        this.gameObject.SetActive(true);
    }

    private void Off()
    {
        this.gameObject.SetActive(false);
    }
}
