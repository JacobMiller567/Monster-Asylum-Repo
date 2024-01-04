using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public AudioClip[] audioClips;
    private AudioSource musicAudio;
    public Slider musicSlider;
    public float musicSliderValue;
    private bool playSound = true;
    private bool active = false;

    // Game Sounds //
    [SerializeField] Toggle playerFootsteps;
    [SerializeField] Toggle jumpScare;
    [SerializeField] Toggle enemyNoise;
    [SerializeField] Toggle gameSound;

    // Gameplay //
    [SerializeField] Toggle lightFlicker;

    // Audio Clips //
    [SerializeField] AudioClip stepsWalk;
    [SerializeField] AudioClip stepsRun;
    [SerializeField] AudioClip stepsCrouch;
    [SerializeField] AudioClip jumpScareNoise;
    [SerializeField] AudioClip enemySniff;
    [SerializeField] AudioClip unlockDoor;
    [SerializeField] AudioClip pickupKeys;


    private void Start()
    {
        musicAudio = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioSequentially());
    }

    private IEnumerator PlayAudioSequentially()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            musicAudio.clip = audioClips[i];
            musicAudio.Play();
            yield return new WaitForSeconds(audioClips[i].length);
        }
    }

    void Update()
    {
        AdjustMusicVolume();
        
        if (Input.GetKeyDown(KeyCode.Escape) && active == false) 
        {
            settingsMenu.SetActive(true);
            active = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && active == true) 
        {
            settingsMenu.SetActive(false);
            active = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AdjustMusicVolume()
    {
        musicSliderValue = musicSlider.value;
        musicAudio.volume = musicSliderValue;
    }



    public void FootStepsToggle()
    {
        if (playSound)
        {
            if (playerFootsteps.isOn == false) 
            {
                MuteFootsteps(true);  
            }

            else 
            {
                MuteFootsteps(false);
            }
        } 

    }
    public void JumpScareToggle() 
    {
        if (playSound)
        {
            if (jumpScare.isOn == false) 
            {
                MuteJumpScare(true);                    
            }
            else
            {
                MuteJumpScare(false);
            }
        }
    }
    public void EnemyNoiseToggle()
    {
        if (playSound)
        {
            if (enemyNoise.isOn == false) 
            {
                MuteEnemy(true);                    
            }

            else
            {
                MuteEnemy(false);
            }
        }
    }
    public void MuteAllSoundsToggle()
    {
        if (gameSound.isOn == false) 
        {
            playSound = false;
            MuteAllSounds(true);                    
        }

        else
        {
            playSound = true;
            MuteAllSounds(false);
        }
    }
    public void DisableLightFlickerToggle()
    {
        if (lightFlicker.isOn == false) 
        {
            DisableLightFlicker(true);                    
        }

        else
        {
            DisableLightFlicker(false);
        }
    }


    public void MuteFootsteps(bool muted)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == stepsWalk)
            {
                audioSource.mute = muted;
            }
            if (audioSource.clip == stepsRun)
            {
                audioSource.mute = muted;
            }
            if (audioSource.clip == stepsCrouch)
            {
                audioSource.mute = muted;
            }
        }
    }

    public void MuteJumpScare(bool muted)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == jumpScareNoise)
            {
                audioSource.mute = muted;
            }
        }
    }

    public void MuteEnemy(bool muted)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == enemySniff)
            {
                audioSource.mute = muted;
            }
        }
    }


    public void MuteDoor(bool muted)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == unlockDoor)
            {
                audioSource.mute = muted;
            }
        }
    }

    public void MuteKeys(bool muted)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == pickupKeys)
            {
                audioSource.mute = muted;
            }
        }
    }

    public void MuteAllSounds(bool muted)
    {
        MuteKeys(muted);
        MuteDoor(muted);
        MuteEnemy(muted);
        MuteJumpScare(muted);
        MuteFootsteps(muted);
    }

    public void DisableLightFlicker(bool on)
    {
        LightFlickerEffect[] lightSources = FindObjectsOfType<LightFlickerEffect>();
        foreach (LightFlickerEffect lightSource in lightSources)
        {
            if (lightSource.disableFlicker == false)
            {
                lightSource.disableFlicker = true;
                lightSource.ChangeLightFlicker();
            }
            else if (lightSource.disableFlicker == true)
            {
                lightSource.disableFlicker = false;
                lightSource.ChangeLightFlicker();
            }
        }
    }
}
