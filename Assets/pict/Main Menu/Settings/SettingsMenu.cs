using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Panel")]
    public GameObject settingsPanel;

    [Header("Audio")]
    public Slider volumeSlider;

    public TMP_Text volumeText;

    void Start()
    {
        settingsPanel.SetActive(false);

        float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        AudioListener.volume = volume;
        volumeSlider.value = volume;

        volumeText.text = Mathf.RoundToInt(volume * 100) + "%";

        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;

        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();

        volumeText.text = Mathf.RoundToInt(value * 100) + "%";
    }
    public void VolumeUp()
    {
        volumeSlider.value = Mathf.Clamp(volumeSlider.value + 0.1f, 0f, 1f);
    }

    public void VolumeDown()
    {
        volumeSlider.value = Mathf.Clamp(volumeSlider.value - 0.1f, 0f, 1f);
    }
}