using UnityEngine;
using UnityEngine.UI;

public class ControllerSlider : MonoBehaviour
{
    [SerializeField] private SettingName _settingName;

    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Start()
    {
        switch (_settingName)
        {
            case SettingName.SoundVolume:
                _slider.value = StorageManager.GetSoundVolume();
                break;
            case SettingName.MusicVolume:
                _slider.value = StorageManager.GetMusicVolume();
                break;
        }
    }
}
