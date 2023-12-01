using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSound;
    [SerializeField] private AudioClip _defaultMusic;
    [SerializeField] private AudioClip _clikcButton;
    [SerializeField] private float _clickButtonVolume = 0.2f;
    public static bool soundVolumeStatus;
    public static bool musicVolumeStatus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSourceMusic = GetComponent<AudioSource>();

    }
    private void Start()
    {
        soundVolumeStatus = false;
        musicVolumeStatus = false;
        SetVolumeSound(soundVolumeStatus);
        SetVolumeMusic(musicVolumeStatus);
    }

    public void SetVolumeSound(bool state)
    {
        musicVolumeStatus = state;
        _audioSourceSound.mute = state;
    }
    public void SetVolumeMusic(bool state)
    {
        soundVolumeStatus = state;
        audioSourceMusic.mute = state;
    }


    public void SetDefaultMusic()
    {
        if (_defaultMusic != null) PlayBGSound(_defaultMusic);
    }

    public void PlaySound(AudioClip sound, float volumeLevel)
    {
        _audioSourceSound.PlayOneShot(sound, volumeLevel);
    }
    public void PlayBGSound(AudioClip _bgSound)
    {
        if (_bgSound != null)
        {
            audioSourceMusic.clip = _bgSound;
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();
        }
    }
    public void ClickButton()
    {
        PlaySound(_clikcButton, _clickButtonVolume);
    }




}

