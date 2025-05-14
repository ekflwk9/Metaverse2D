using UnityEngine;

public class StartMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On);
        GameManager.gameEvent.Add(Off);
    }

    private void On() => this.gameObject.SetActive(true);
    private void Off() => this.gameObject.SetActive(false);
}
