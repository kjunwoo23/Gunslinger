using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoPlayer videoPlayer2;
    public AudioSource startEffect;

    public GameObject MainMenuUI;
    public GameObject loadingText;
    bool loading;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MenuAppear());
        startEffect.volume = 0.5f;
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
