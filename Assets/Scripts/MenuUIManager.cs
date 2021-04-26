using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuUIManager : MonoBehaviour
{
    void Awake()
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
        
    void OnDestroy()
    {
    
    }
}