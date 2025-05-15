using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSoundEventHandler : MonoBehaviour
{
    public string[] soundName;

    public void PlaySound()
    {
        var randomSound = Random.Range(0, soundName.Length);
        
        GameManager.sound.OnEffect(soundName[randomSound]);
    }
}