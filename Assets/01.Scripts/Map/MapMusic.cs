using UnityEngine;

public class MapMusic : MonoBehaviour
{
    [SerializeField] private string musicName;

    private void Awake()
    {
        if (!string.IsNullOrEmpty(musicName))
        {
            GameManager.sound.OnMusic(musicName);
        }
    }
}
