using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MusicPlayerController : MonoBehaviour
{
    public static MusicPlayerController instance;

    public AudioSource audioSource;
    [SerializeField] List<MusicClip> clips; // list of MusicClip scriptableObjects

    [Header("Music clip info")]
    [SerializeField] TextMeshProUGUI songName;
    [SerializeField] TextMeshProUGUI artist;
    [SerializeField] TextMeshProUGUI album;
    [SerializeField] Image icon;

    // index to get clips from list
    [SerializeField] int index = 0;

    [Header("Time and Volume ")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI runTime;
    [SerializeField] TextMeshProUGUI elapsedTime;
    [SerializeField] TextMeshProUGUI remainingTime;
    [SerializeField] TextMeshProUGUI volumeText;

    [Header("Buttons")]
    [SerializeField] Button playButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button stopButton;

    [SerializeField] GameObject visualizerPanel;
    [SerializeField] GameObject infoPanel;

    int lastTimeCheck;
    float volumeSetting;
    bool isPaused;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        GetAudioClip(index);
        SetDefaultValues();
     

    }

    private void Update()
    {
        DisplayTime();
        SetVolume();
    }
    private void SetDefaultValues()
    {
        // when music clip is not playing only start button is active
        stopButton.interactable = false;
        pauseButton.interactable = false;
        playButton.interactable = true;

        // volume is set to half of max value by default
        volumeSlider.value = 0.5f;
        audioSource.volume = 0.5f;
        volumeSetting = 0.5f;
    }
    void GetAudioClip(int index)
    {
        // set current clip to play
        audioSource.clip = clips[index].GetAudioClip();

        lastTimeCheck = 0;

        // set time display
        runTime.text = GetTime(clips[index].GetAudioClip().length);
        elapsedTime.text = GetTime(clips[index].GetAudioClip().length);
        remainingTime.text = GetTime(clips[index].GetAudioClip().length);

        // set clip content
        songName.text = clips[index].GetName();
        artist.text = clips[index].GetArtist();
        album.text = clips[index].GetAlbum();
        icon.sprite = clips[index].GetSprite();
        
    }

    // display length of audioclip, elapsed time and time remaining
    private string GetTime(double clipLength)
    {
        int totalClipLenght = (int)clipLength;
        int clipHours = 0;
        int clipMinutes = 0;
        int clipSeconds = 0;

        string displauHours;
        string displayMinutes;
        string displaySeconds;

        // if clip is longer or equal to one hour
        if(totalClipLenght >= 3600)
        {
            clipHours = totalClipLenght / 3600; // seconds in hour
            clipMinutes = (totalClipLenght - (clipHours * 3600)) / 60;
            clipSeconds = totalClipLenght - ((clipHours * 3600) + (clipMinutes * 60));
        }
        // if clip is longer or equal to one minute
        else if(totalClipLenght >= 60)
        {
            clipMinutes = totalClipLenght / 60;
            clipSeconds = totalClipLenght - (clipMinutes * 60);
        }
        // if clip is less than minute
        else
        {
            clipSeconds = totalClipLenght;
        }

        // display hours, minutes and seconds as two digits
        displauHours = clipHours.ToString("D2");
        displayMinutes = clipMinutes.ToString("D2");
        displaySeconds = clipSeconds.ToString("D2");

        return ($"{displauHours}:{displayMinutes}:{displaySeconds}");
    }

    private void DisplayTime()
    {
        if (audioSource.isPlaying && (int)audioSource.time != lastTimeCheck)
        {
            elapsedTime.text = GetTime(audioSource.time);
            remainingTime.text = GetTime(clips[index].GetAudioClip().length - audioSource.time);
            lastTimeCheck = (int)audioSource.time;
        }
    }
    private void SetVolume()
    {
        if (volumeSlider.value != volumeSetting)
        {
            audioSource.volume = volumeSlider.value;
            volumeText.text = ((int)(volumeSlider.value * 100)).ToString("D2");

        }

    }

    /// <summary>
    /// Functions for buttons
    /// </summary>
    public void PlayClip()
    {
        audioSource.Play();
        isPaused = false;
        pauseButton.interactable = true;
        stopButton.interactable = true;
        playButton.interactable = false;
    }
    public void PauseClip()
    {
        if (!isPaused)
        {
            audioSource.Pause();
            pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "UnPause";
            isPaused = true;
        }
        else
        {
            audioSource.Play();
            pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
            isPaused = false;
        }
        
       
    }
    public void StopClip()
    {
        audioSource.Stop();
        isPaused = false;
    }

    public void NextClip()
    {
         index++;
        if (index > clips.Count - 1)
            index = clips.Count - 1;

        
        GetAudioClip(index);
    }
    public void PreviousClip()
    {
        index--;
        if (index < 0)
            index = 0;

        GetAudioClip(index);
    }
    public void VisualizeButton()
    {
        visualizerPanel.SetActive(true);
        infoPanel.SetActive(false);
    }
    public void BackButton()
    {
        visualizerPanel.SetActive(false);
        infoPanel.SetActive(true);
    }





}
