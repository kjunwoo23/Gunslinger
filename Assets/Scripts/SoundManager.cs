using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Slider slider;
    public AudioSource bgmPlayer;
    public AudioLowPassFilter lowPassFilter;
    public Sound[] bgmSounds;
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey("Restart"))
            if (PlayerPrefs.GetInt("Restart") == 1)
            {
                PlayerPrefs.SetInt("Restart", 0);
                Destroy(gameObject);
            }
        instance = this;
        /*if (PlayerPrefs.HasKey("SoundVolume"))
        {
            bgmPlayer.volume = PlayerPrefs.GetFloat("SoundVolume");
            slider.value = bgmPlayer.volume;
        }
        else
            bgmPlayer.volume = 0.5f;*/
        //bgmPlayer.clip = bgmSounds[1].clip;
        //bgmPlayer.Play();
        //text.text = bgmPlayer.volume.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
        
    }
    public void SetBgmVolume(Slider slider)
    {
        float value = Mathf.Round(slider.value * 100) * 0.01f;
        bgmPlayer.volume = value;
        PlayerPrefs.SetFloat("SoundVolume", value);
        //text.text = value.ToString();
        //if (bgmPlayer.clip == bgmSounds[3].clip)
        //bgmPlayer.volume = slider.value * 0.5f;
    }
}