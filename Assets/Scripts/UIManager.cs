using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{
    void Awake()
    {
        
    }

    void Start()
    {

    }

    public static event Action<bool> OnSoundToggle;
    public static event Action<bool> OnMusicToggle;
    public static event Action<float> OnSoundChange;
    public static event Action<float> OnMusicChange;
    public static event Action<bool> OnPause;

    public void SoundToggle(Toggle toggle)
    {
        OnSoundToggle?.Invoke(toggle.isOn);
    }

    public void MusicToggle(Toggle toggle)
    {
        
        OnMusicToggle?.Invoke(toggle.isOn);
    }

    public void SoundChange(Slider slider)
    {
        OnSoundChange?.Invoke(slider.value);
    }

    public void MusicChange(Slider slider)
    {
        OnMusicChange?.Invoke(slider.value);
    }

    // main menu
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // game
    public void Pause(bool value)
    {
        OnPause?.Invoke(value);
    }

    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }

    void OnDestroy()
    {

    }
}

public enum SettingName
{
    MusicOn,
    SoundOn,
    SoundVolume,
    MusicVolume
}