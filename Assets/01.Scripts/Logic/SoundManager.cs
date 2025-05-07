using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private Dictionary<string, AudioClip> sound = new Dictionary<string, AudioClip>();

    private EffectSound effect;
    private MusicSound music;

    public void Load()
    {
        var sounds = Resources.LoadAll<AudioClip>("Sound");

        for (int i = 0; i < sounds.Length; i++)
        {
            sound.Add(sounds[i].name, sounds[i]);
        }
    }

    public void SetComponent(MonoBehaviour _component)
    {
        if (_component is EffectSound isEffect) effect = isEffect;
        else if (_component is MusicSound isMusic) music = isMusic;
    }

    public void OnEffect(string _soundName) 
    {
        if (sound.ContainsKey(_soundName)) effect.On(sound[_soundName]);
        else Debug.Log($"{_soundName}은 Resources/Sound에 추가되지 않은 사운드");
    }

    public void OnMusic(string _soundName)
    {
        if(string.IsNullOrEmpty(_soundName)) music.On(null);
        else if (sound.ContainsKey(_soundName)) music.On(sound[_soundName]);
        else Debug.Log($"{_soundName}은 Resources/Sound에 추가되지 않은 사운드");
    }
}
