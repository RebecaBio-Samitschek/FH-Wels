using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    
    public AudioSource sound;
    public AudioClip clip;
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume",1);
            Load();
        } else {
            Load();
        }

        volumeSlider.onValueChanged.AddListener(delegate { OnSliderRelease(); });
    }

    void OnSliderRelease()
    {
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    private void Load() {
    volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
