using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip knife;
    [SerializeField] AudioClip sheath;
    [SerializeField] AudioClip embed;
    [SerializeField] AudioClip death;
    AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlayKnife()
    {
        src.Stop();
        src.PlayOneShot(knife);
    }

    public void PlaySheath()
    {
        src.Stop();
        src.PlayOneShot(sheath);
    }

    public void PlayEmbed()
    {
        src.Stop();
        src.PlayOneShot(embed);
    }

    public void PlayDeath()
    {
        src.Stop();
        src.PlayOneShot(death);
    }
}
