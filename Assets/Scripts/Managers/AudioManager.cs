using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip birdHappyLoop;
    [SerializeField] private AudioClip birdHopSFX;
    [SerializeField] private AudioClip pigDefeatedSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayHappyLoop()
    {
        if (bgSource.clip == birdHappyLoop && bgSource.isPlaying)
            return;

        bgSource.clip = birdHappyLoop;
        bgSource.loop = true;
        bgSource.Play();
    }

    public void StopBackground()
    {
        bgSource.Stop();
    }

    public void PlayBirdHop()
    {
        sfxSource.PlayOneShot(birdHopSFX);
    }

    public void PlayPigDefeated()
    {
        sfxSource.PlayOneShot(pigDefeatedSFX);
    }
}
