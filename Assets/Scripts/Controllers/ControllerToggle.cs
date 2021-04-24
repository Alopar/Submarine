using UnityEngine;
using UnityEngine.UI;

public class ControllerToggle : MonoBehaviour
{   
    [SerializeField] private Sprite _toggleSpriteOn;
    [SerializeField] private Sprite _toggleSpriteOff;
    [SerializeField] private SettingName _settingName;

    private Image _image;
    private Toggle _toggle;

    void Awake()
    {
        _image = GetComponent<Image>();
        _toggle = GetComponent<Toggle>();
    }

    void Start()
    {
        switch (_settingName)
        {
            case SettingName.SoundOn:
                _toggle.isOn = StorageManager.GetSoundState();
                break;
            case SettingName.MusicOn:
                _toggle.isOn = StorageManager.GetMusicState();
                break;
        }
    }

    public void ToggleChange()
    {
        _image.sprite = _toggle.isOn ? _toggleSpriteOn : _toggleSpriteOff;
    }
}