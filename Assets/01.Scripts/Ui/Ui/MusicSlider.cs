using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0.5f;
    }

    public void SetMusicVolume()
    {
        GameManager.sound.SetMusicVolume(slider.value);
    }
}
