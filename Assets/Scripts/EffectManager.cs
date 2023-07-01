using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Effect
{
    public string soundName;
    public AudioClip clip;
    public AudioSource source;
}
public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public Slider slider;
    public Effect[] effectSounds;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        float tmp = 0.5f;
        /*if (PlayerPrefs.HasKey("EffectVolume"))
        {
            //Debug.Log(PlayerPrefs.GetFloat("EffectVolume"));
            //tmp = 0.5f;
            tmp = PlayerPrefs.GetFloat("EffectVolume");
            //slider.value = tmp;
        }
        else
            tmp = 0.5f;
        */
        for (int i = 0; i < effectSounds.Length; i++)
        {
            effectSounds[i].source = gameObject.AddComponent<AudioSource>();
            effectSounds[i].source.clip = effectSounds[i].clip;
            effectSounds[i].source.loop = false;
            effectSounds[i].source.volume = tmp;

            if (i == 2 || i == 3)
                effectSounds[i].source.volume *= 0.5f;
        }

        //slider.value = tmp;
        //text.text = tmp.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetEffectVolume(Slider slider)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            float value = Mathf.Round(slider.value * 100) * 0.01f;
            effectSounds[i].source.volume = value;
            
            PlayerPrefs.SetFloat("EffectVolume", value);
            //text.text = value.ToString();
        }
        if (Time.timeScale == 0f)
            effectSounds[1].source.Play();
    }

    public void MuteAll()
    {
        for (int i = 0; i < effectSounds.Length; i++)
            effectSounds[i].source.mute = true;
    }
    public void MuteAll(int idx)
    {
        for (int i = 0; i < effectSounds.Length; i++)
            effectSounds[i].source.mute = true;
        effectSounds[idx].source.mute = false;
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        effectSounds[34].source.Play();
    }
    public void OnPointerUp(BaseEventData eventData)
    {
        effectSounds[34].source.Stop();
    }
}