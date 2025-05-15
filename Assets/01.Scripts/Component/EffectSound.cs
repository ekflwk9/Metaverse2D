using UnityEngine;

public class EffectSound : MonoBehaviour
{
    private int soundCount = 10;
    private AudioSource[] source;

    private void Awake()
    {
        source = new AudioSource[soundCount];

        for (int i = 0; i < soundCount; i++)
        {
            source[i] = this.gameObject.AddComponent<AudioSource>();

            source[i].loop = false;
            source[i].playOnAwake = false;
            source[i].volume = 0.5f;
        }

        GameManager.SetComponent(this);
    }

    public void SetVolume()
    {
        //º¼·ý ¼³Á¤
        for (int i = 0; i < soundCount; i++)
        {
            source[i].volume = GameManager.sound.effectVolume;
        }
    }

    public void On(AudioClip _sound)
    {
        soundCount++;
        if (soundCount >= source.Length) soundCount = 0;

        source[soundCount].volume = GameManager.sound.effectVolume;
        source[soundCount].clip = _sound;
        source[soundCount].Play();
    }
}
