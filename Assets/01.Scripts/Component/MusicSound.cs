using UnityEngine;

public class MusicSound : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = this.gameObject.AddComponent<AudioSource>();

        source.loop = true;
        source.playOnAwake = false;
        source.volume = 0.5f;

        GameManager.SetComponent(this);
    }

    public void On(AudioClip _sound)
    {
        if (_sound != null)
        {
            source.clip = _sound;
            source.Play();
        }

        else
        {
            source.Stop();
        }
    }
}
