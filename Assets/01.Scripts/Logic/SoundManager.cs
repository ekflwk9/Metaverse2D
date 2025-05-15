using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public float musicVolume { get; private set; } = 0.5f;
    public float effectVolume { get; private set; } = 0.5f;

    private EffectSound effect;
    private MusicSound music;

    private Dictionary<string, AudioClip> sound = new Dictionary<string, AudioClip>();

    public void Load()
    {
        var sounds = Resources.LoadAll<AudioClip>("Sound");

        for (int i = 0; i < sounds.Length; i++)
        {
            sound.Add(sounds[i].name, sounds[i]);
        }
    }

    public void SetMusicVolume(float _volume)
    {
        //볼륨 설정
        musicVolume = _volume;
        music.SetVolume();
    }

    public void SetEffectVolume(float _volume)
    {
        effectVolume = _volume;
        effect.SetVolume();
    }

    public void SetComponent(MonoBehaviour _component)
    {
        if (_component is EffectSound isEffect) effect = isEffect;
        else if (_component is MusicSound isMusic) music = isMusic;
    }

    public void OnEffect(string _soundName) 
    {
        if (sound.ContainsKey(_soundName)) effect.On(sound[_soundName]);
        else Service.Log($"{_soundName}은 Resources/Sound에 추가되지 않은 사운드");
    }

    public void OnMusic(string _soundName)
    {
        if(string.IsNullOrEmpty(_soundName)) music.On(null);
        else if (sound.ContainsKey(_soundName)) music.On(sound[_soundName]);
        else Service.Log($"{_soundName}은 Resources/Sound에 추가되지 않은 사운드");
    }
}
