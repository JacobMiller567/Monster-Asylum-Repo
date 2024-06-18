using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MusicManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public AudioClip[] audioClips;
    private AudioSource musicAudio;
    public Slider musicSlider;
    public float musicSliderValue;
    private bool gameSoundEnabled = true;
    private bool active = false;

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
    [SerializeField] AudioClip playerHeartBeat;
    [SerializeField] AudioClip pickupNote;
    [SerializeField] AudioClip keypadClick;
    [SerializeField] AudioClip wrongInput;
    [SerializeField] AudioClip correctInput;


    [SerializeField] private AudioClip[] gameSounds;
    [SerializeField] private float[] gameVolumeDefault;



    private void Start()
    {
        musicAudio = GetComponent<AudioSource>();
       // SetDefaultVolume(); 
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
            //MuteAllSounds(1);

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && active == true) 
        {
            settingsMenu.SetActive(false);
            active = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
           // MuteAllSounds(0);
        }
    }

    public void AdjustMusicVolume()
    {
        musicSliderValue = musicSlider.value;
        musicAudio.volume = musicSliderValue;
    }


/*
    public void SetDefaultVolume() // Set initial game volume
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            for (int i = 0; i < gameSounds.Length; i++)
            {
                audioSource.volume = gameVolumeDefault[i];
            }
        }

    }
    public void LoadDefaultVolume() // Reset game volume back to default
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            for (int i = 0; i < gameSounds.Length; i++)
            {
                audioSource.volume = gameVolumeDefault[i];
            }
        }
    }
    public void AdjustGameVolume() // Adjust game volume
    {
        gameSoundSliderValue = gameSoundSlider.value;

        for (int i = 0; i < gunSounds.Length; i++)
        {
            gunSounds[i].volume = gameSoundSliderValue;
        }
        for (int i = 0; i < playerSounds.Length; i++)
        {
            playerSounds[i].volume = gameSoundSliderValue;
        }
        for (int i = 0; i < zombieSounds.Length; i++)
        {
            zombieSounds[i].volume = gameSoundSliderValue;
        }
    }
*/


    public void MuteFootsteps(TMP_Dropdown dropdown)
    {
        bool isMuted = dropdown.value == 1;
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (!gameSoundEnabled) return;
            
            if (audioSource.clip == stepsWalk)
            {
                audioSource.mute = isMuted;
            }
            if (audioSource.clip == stepsRun)
            {
                audioSource.mute = isMuted;
            }
            if (audioSource.clip == stepsCrouch)
            {
                audioSource.mute = isMuted;
            }
        }
    }

    public void MuteJumpScare(TMP_Dropdown dropdown)
    {
        bool isMuted = dropdown.value == 1;
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == jumpScareNoise)
            {
                audioSource.mute = isMuted;
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
            if (audioSource.clip == jumpScareNoise)
            {
                audioSource.mute = muted;
            }
        }
    }
    public void MutePlayer(bool muted)
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
            if (!audioSource.clip == playerHeartBeat)
            {
                audioSource.mute = muted;
            }
        }
    }

    public void MuteInteraction(bool muted)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == pickupKeys)
            {
                audioSource.mute = muted;
            }
            if (audioSource.clip == pickupNote)
            {
                audioSource.mute = muted;
            }
            if (audioSource.clip == unlockDoor)
            {
                audioSource.mute = muted;
            }
        }
    }

    public void MuteCombination(bool muted)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == keypadClick)
            {
                audioSource.mute = muted;
            }
            if (audioSource.clip == correctInput)
            {
                audioSource.mute = muted;
            }
            if (audioSource.clip == wrongInput)
            {
                audioSource.mute = muted;
            }
        }
    }



    public void MuteAllSounds(TMP_Dropdown dropdown)
    {
        bool isMuted = dropdown.value == 1;
        if (isMuted == true)
        {
            gameSoundEnabled = false;
        }
        else
        {
            gameSoundEnabled = true;
        }
        MuteInteraction(isMuted);
        MuteEnemy(isMuted);
        MutePlayer(isMuted);
        MuteCombination(isMuted);
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
