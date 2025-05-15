using UnityEngine;
using UnityEngine.UI;

public class EffectSlider : MonoBehaviour
{
    private Slider slider;

    public void SetEffectVolume()
    {
        if (slider != null)
        {
            GameManager.sound.SetEffectVolume(slider.value);
        }

        else
        {
            slider = GetComponent<Slider>();
            slider.value = 0.5f;
        }
    }
}
