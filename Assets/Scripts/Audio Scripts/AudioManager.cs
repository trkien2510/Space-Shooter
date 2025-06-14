using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource bulletSFXSource;
    public AudioSource explosionSFXSource;

    [Header("Audio Clips")]
    public AudioClip bgMusic;
    public AudioClip laserBullet;
    public AudioClip explosionSound;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (bgmSource != null)
        {
            bgmSource.clip = bgMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        float bgmVol = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetBGMVolume(bgmVol);
        SetSFXVolume(sfxVol);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayBulletSFX()
    {
        if (bulletSFXSource != null && laserBullet != null)
            bulletSFXSource.PlayOneShot(laserBullet);
    }

    public void PlayExplosionSFX()
    {
        if (explosionSFXSource != null && explosionSound != null)
            explosionSFXSource.PlayOneShot(explosionSound);
    }

    public void SetBGMVolume(float sliderValue)
    {
        if (audioMixer == null)
        {
            return;
        }
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("BulletVolume", volume);
        PlayerPrefs.SetFloat("BulletVolume", sliderValue);
        audioMixer.SetFloat("ExplosionVolume", volume);
        PlayerPrefs.SetFloat("ExplosionVolume", sliderValue);
    }
}
