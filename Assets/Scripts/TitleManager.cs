using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoPlayer videoPlayer2;
    public AudioSource startEffect;

    public GameObject MainMenuUI;
    public GameObject loadingText;
    bool loading;

    public TMP_InputField nickname;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI played;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        StartCoroutine(MenuAppear());
        startEffect.volume = 0.5f;

        if (PlayerPrefs.HasKey("Nickname"))
            nickname.text = PlayerPrefs.GetString("Nickname");

        if (PlayerPrefs.HasKey("Highscore"))
            highScore.text = PlayerPrefs.GetInt("Highscore").ToString();
        else
            highScore.text = "0";

        if (PlayerPrefs.HasKey("Played"))
            played.text = "Cleared: " + PlayerPrefs.GetInt("Played").ToString();
        else
            played.text = "Cleared: 0";

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(videoPlayer.time);
    }

    IEnumerator MenuAppear()
    {
        yield return new WaitUntil(() => videoPlayer.time >= 5.5f);
        //yield return new WaitForSeconds(6);
        //videoPlayer.SetDirectAudioMute(0, true);
        MainMenuUI.SetActive(true);
        startEffect.Play();
        // videoPlayer.Pause();
        /*while (true)
        {
            if (videoPlayer.playbackSpeed > 0.001f)
                videoPlayer.playbackSpeed *= 0.5f;
            yield return new WaitForSeconds(0.1f);
        }*/
    }
    
    public void OnTypeNickname()
    {
        PlayerPrefs.SetString("Nickname", nickname.text);
    }

    public void OnClickStart()
    {
        if (loading) return;
        loading = true;
        StartCoroutine(LoadToIngame());
    }

    IEnumerator LoadToIngame()
    {
        MainMenuUI.SetActive(false);
        loadingText.SetActive(true);
        videoPlayer2.Play();
        yield return new WaitForSeconds(5);
        yield return new WaitWhile(() => videoPlayer2.isPlaying);
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");
#else
        Application.Quit();
#endif
    }
}
