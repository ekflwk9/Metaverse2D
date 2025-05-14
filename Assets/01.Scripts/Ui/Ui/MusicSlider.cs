using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private Slider slider;

    public void SetMusicVolume()
    {
        if (slider != null)
        {
            GameManager.sound.SetMusicVolume(slider.value);
        }

        else
        {
            slider = GetComponent<Slider>();
            slider.value = 0.5f;
        }
    }
}
