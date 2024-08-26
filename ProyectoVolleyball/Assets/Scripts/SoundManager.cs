using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    [Header("Sounds")]
    [SerializeField]
    Sound[] sounds;

    [Header("Buttons")]
    [SerializeField]
    Button musicButton;
    [SerializeField]
    Button sfxButton;

    [Header("Sliders")]
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;

    [Header("Sprites")]
    [SerializeField]
    Sprite musicMutedSprite;
    [SerializeField]
    Sprite musicUnmutedSprite;
    [SerializeField]
    Sprite sfxMutedSprite;
    [SerializeField]
    Sprite sfxUnmutedSprite;

    AudioSource _musicSource;
    AudioSource _sfxSource;

    bool _isMusicMuted = false;
    bool _isSfxMuted = false;

    private void Awake()
    {
        _instance = this;

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.volume = 0.2F;

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.volume = 0.2F;

        if (musicSlider != null)
        {
            musicSlider.value = 0.2F;
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = 0.2F;
        }
    }

    public static SoundManager Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        PlayMusic("Music");
    }

    private Sound FindSound(string name)
    {
        return Array.Find(sounds, s => s.GetName().Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    #region Play
    public void PlayMusic(string name)
    {
        Sound music = FindSound(name);
        if (music == null)
        {
            return;
        }

        _musicSource.loop = true;
        _musicSource.clip = music.GetAudio();
        _musicSource.Play();
    }

    public void PlaySFX(string name, bool loop = false)
    {
        Sound sfx = FindSound(name);
        if (sfx == null)
        {
            return;
        }

        if (loop)
        {
            _sfxSource.loop = loop;
            _sfxSource.clip = sfx.GetAudio();
            _sfxSource.Play();
        }
        else
        {
            _sfxSource.PlayOneShot(sfx.GetAudio());
        }
    }
    #endregion

    #region Mute UnMute

    public void MuteUnMuteMusic()
    {
        _isMusicMuted = !_isMusicMuted;
        _musicSource.volume = _isMusicMuted ? 0F : 1F;

        if (musicSlider != null)
        {
            musicSlider.value = _isMusicMuted ? 0F : 1F;
        }

        if (musicButton != null)
        {
            musicButton.GetComponent<Image>().sprite = _isMusicMuted ? musicMutedSprite : musicUnmutedSprite;
        }
    }

    public void MuteUnMuteSFX()
    {
        _isSfxMuted = !_isSfxMuted;
        _sfxSource.volume = _isSfxMuted ? 0F : 1F;

        if (sfxSlider != null)
        {
            sfxSlider.value = _isSfxMuted ? 0F : 1F;
        }

        if (sfxButton != null)
        {
            sfxButton.GetComponent<Image>().sprite = _isSfxMuted ? sfxMutedSprite : sfxUnmutedSprite;
        }
    }

    #endregion

    #region Slider

    public void SetMusicVolume()
    {
        if (musicSlider != null)
        {
            _musicSource.volume = musicSlider.value;
            if (musicSlider.value == 0F)
            {
                _isMusicMuted = true;
                if (musicButton != null)
                {
                    musicButton.GetComponent<Image>().sprite = musicMutedSprite;
                }
            }
            if (musicSlider.value == 1F)
            {
                _isMusicMuted = false;
                if (musicButton != null)
                {
                    musicButton.GetComponent<Image>().sprite = musicUnmutedSprite;
                }
            }
        }
    }

    public void SetSFXVolume()
    {
        if (sfxSlider != null)
        {
            _sfxSource.volume = sfxSlider.value;
            if (sfxSlider.value == 0F)
            {
                _isSfxMuted = true;
                if (sfxButton != null)
                {
                    sfxButton.GetComponent<Image>().sprite = sfxMutedSprite;
                }
            }
            if (sfxSlider.value == 1F)
            {
                _isSfxMuted = false;
                if (sfxButton != null)
                {
                    sfxButton.GetComponent<Image>().sprite = sfxUnmutedSprite;
                }
            }
        }
    }
    #endregion
}
