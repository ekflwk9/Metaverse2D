using UnityEngine;
using UnityEngine.UI;

public class EffectSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0.5f;
    }

    public void SetEffectVolume()
    {
        GameManager.sound.SetEffectVolume(slider.value);
    }
}
