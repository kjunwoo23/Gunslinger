using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESCManager : MonoBehaviour
{
    public static ESCManager instance;

    public GameObject escMenu;

    public RawImage[] spring;

    private void Awake()
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (CutSceneManager.instance.changePhaseCor == null && !ScoreManager.instance.gameOver)
                if (escMenu.activeSelf)
                    ESCDisappear();
                else
                    ESCAppear();

        spring[0].rectTransform.offsetMax = new Vector2(-160 * SoundManager.instance.slider.value, -11);
        spring[1].rectTransform.offsetMax = new Vector2(-160 * EffectManager.instance.slider.value, -11);

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void ESCAppear()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        EffectManager.instance.MuteAll(1);
        escMenu.SetActive(true);
    }

    public void ESCDisappear()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        EffectManager.instance.UnMuteAll();
        escMenu.SetActive(false);
    }

    public void OnClickRestart()
    {
        ScoreManager.instance.Restart();
    }

    public void OnClickToMenu()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        SceneManager.LoadScene("TitleScene");
    }
}
