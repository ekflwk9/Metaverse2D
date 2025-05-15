using UnityEngine;

public class MenuWindow : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameEvent.Add(On, true);
        GameManager.gameEvent.Add(Off, true);
    }

    private void On() => this.gameObject.SetActive(true);

    private void Off() => this.gameObject.SetActive(false); 
}
