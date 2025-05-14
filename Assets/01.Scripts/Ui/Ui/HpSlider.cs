using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = GameManager.player.maxHealth;
        slider.value = slider.maxValue;

        GameManager.gameEvent.Add(On, true);
        GameManager.gameEvent.Add(Off, true);
        GameManager.gameEvent.Add(SliderUpdate, true);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    ///�÷��̾� HP�� �°� �����̴��� �����ϴ� �޼���
    /// </summary>
    public void SliderUpdate() => slider.value = GameManager.player.health;
    private void On() => this.gameObject.SetActive(true);
    private void Off() => this.gameObject.SetActive(false);
}
