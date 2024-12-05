using UnityEngine;

/// <summary>
/// Audio Manager to play SFX
/// </summary>

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _ballHit;
    [SerializeField] private AudioClip _ballShot;

    private static AudioManager _singleton;

    private void Awake()
    {
        _singleton = this;
    }

    public static AudioManager Get()
    {
        return _singleton;
    }

    public void PlayShotSFX()
    {
        PlayOnce(_ballShot);
    }

    public void PlayHitSFX()
    {
        PlayOnce(_ballHit);
    }

    public void PlayOnce(AudioClip clip)
    {
        GameObject go = new GameObject();
        AudioSource source = go.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = clip;
        source.Play();
        Destroy(go, clip.length);
    }
}
