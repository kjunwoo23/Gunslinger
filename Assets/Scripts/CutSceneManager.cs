using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager instance;

    public VideoPlayer videoPlayer;
    public VideoPlayer[] clips;

    public GameObject inGameUI;
    public GameObject skipUI;
    public Animator cutSceneUIAnimator;

    public TextMeshProUGUI phaseScoreUI;

    public Coroutine changePhaseCor;

    public TextMeshProUGUI[] texts;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartToPhase1();
    }

    // Update is called once per frame
    void Update()
    {
        if (changePhaseCor == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartToPhase1();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                StartToPhase2();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                StartToPhase3();
            if (Input.GetKeyDown(KeyCode.Alpha4))
                StartGameClear();
        }
    }

    public void StartToPhase1()
    {
        changePhaseCor = StartCoroutine(ToPhase1());
    }
    public void StartToPhase2()
    {
        changePhaseCor = StartCoroutine(ToPhase2());
    }
    public void StartToPhase3()
    {
        changePhaseCor = StartCoroutine(ToPhase3());
    }
    public void StartGameClear()
    {
        changePhaseCor = StartCoroutine(GameClear());
    }

    public IEnumerator ToPhase1()
    {
        //yield return new WaitForSeconds(2);
        cutSceneUIAnimator.SetBool("appear", true);
        //float t = 46.5f;
        PhaseManager.instance.cutScenePlaying = true;
        inGameUI.SetActive(false);
        videoPlayer.enabled = true;
        videoPlayer.clip = clips[0].clip;
        videoPlayer.Play();
        yield return new WaitForSeconds(1.5f);
        skipUI.SetActive(true);
        while (videoPlayer.isPlaying) {
            /*if (videoPlayer.time > 23f)
                texts[2].enabled = false;
            else if (videoPlayer.time > 17f)
                texts[2].enabled = true;
            else if (videoPlayer.time > 12f)
            {
                texts[0].enabled = false;
                texts[1].enabled = false;
            }
            else if (videoPlayer.time > 7.5f)
            {
                texts[0].enabled = true;
                texts[1].enabled = true;
            }*/
            if (Input.GetKey(KeyCode.Escape)) break;
            //t -= Time.deltaTime;
            yield return null;
        }
        texts[0].enabled = false;
        texts[1].enabled = false;
        texts[2].enabled = false;
        SoundManager.instance.MoveTime(183.5f);
        videoPlayer.enabled = false;
        skipUI.SetActive(false);
        inGameUI.SetActive(true);
        PhaseManager.instance.ChangeToPhase1();
        cutSceneUIAnimator.SetBool("appear", false);
        changePhaseCor = null;
    }

    public IEnumerator ToPhase2()
    {
        yield return new WaitForSeconds(3);
        inGameUI.SetActive(false);
        cutSceneUIAnimator.SetBool("appear", true);
        yield return new WaitForSeconds(2);
        //float t = 14.3f;
        PhaseManager.instance.cutScenePlaying = true;
        PlayerShoot.instance.curRifleBullet--;
        videoPlayer.enabled = true;
        videoPlayer.clip = clips[1].clip;
        videoPlayer.Play();
        yield return new WaitForSeconds(3);
        skipUI.SetActive(true);
        while (videoPlayer.isPlaying)
        {
            if (Input.GetKey(KeyCode.Escape)) break;
            //t -= Time.deltaTime;
            yield return null;
        }
        ScoreManager.instance.phaseScore[0] = ScoreManager.instance.score;
        SoundManager.instance.MoveTime(62.4f);
        videoPlayer.enabled = false;
        skipUI.SetActive(false);
        inGameUI.SetActive(true);
        PhaseManager.instance.ChangeToPhase2();
        cutSceneUIAnimator.SetBool("appear", false);
        changePhaseCor = null;
    }

    public IEnumerator ToPhase3()
    {
        yield return new WaitForSeconds(3);
        inGameUI.SetActive(false);
        cutSceneUIAnimator.SetBool("appear", true);
        yield return new WaitForSeconds(2);
        //float t = 36.3f;
        PhaseManager.instance.cutScenePlaying = true;
        PlayerShoot.instance.curRevolverBullet--;
        videoPlayer.enabled = true;
        videoPlayer.clip = clips[2].clip;
        videoPlayer.Play();
        yield return new WaitForSeconds(3);
        skipUI.SetActive(true);
        while (videoPlayer.isPlaying)
        {
            if (Input.GetKey(KeyCode.Escape)) break;
            //t -= Time.deltaTime;
            yield return null;
        }
        ScoreManager.instance.phaseScore[1] = ScoreManager.instance.score - ScoreManager.instance.phaseScore[0];
        SoundManager.instance.MoveTime(169f);
        videoPlayer.enabled = false;
        skipUI.SetActive(false);
        inGameUI.SetActive(true);
        PhaseManager.instance.ChangeToPhase3();
        cutSceneUIAnimator.SetBool("appear", false);
        changePhaseCor = null;
    }

    public IEnumerator GameClear()
    {
        yield return new WaitForSeconds(3);
        inGameUI.SetActive(false);
        cutSceneUIAnimator.SetBool("appear", true);
        yield return new WaitForSeconds(2);
        //float t = 0;
        PhaseManager.instance.cutScenePlaying = true;
        //skipUI.SetActive(true);
        videoPlayer.enabled = true;
        videoPlayer.clip = clips[3].clip;
        videoPlayer.Play();
        ScoreManager.instance.phaseScore[2] = ScoreManager.instance.score - ScoreManager.instance.phaseScore[0] - ScoreManager.instance.phaseScore[1];

        if (PlayerPrefs.HasKey("Highscore"))
        {
            if (PlayerPrefs.GetInt("Highscore") < ScoreManager.instance.score)
                PlayerPrefs.SetInt("Highscore", ScoreManager.instance.score);
        }
        else
            PlayerPrefs.SetInt("Highscore", ScoreManager.instance.score);

        if (PlayerPrefs.HasKey("Played"))
            PlayerPrefs.SetInt("Played", PlayerPrefs.GetInt("Played") + 1);
        else
            PlayerPrefs.SetInt("Played", 1);

        while (PhaseManager.instance.cutScenePlaying)
        {
            if (videoPlayer.time > 20.58f)
                phaseScoreUI.text = "Total Score\n" + ScoreManager.instance.score.ToString();
            else if (videoPlayer.time > 18.13f)
                phaseScoreUI.text = "Phase 3\n" + ScoreManager.instance.phaseScore[2].ToString();
            else if (videoPlayer.time > 15.33f)
                phaseScoreUI.text = "Phase 2\n" + ScoreManager.instance.phaseScore[1].ToString();
            else if (videoPlayer.time > 12.56f)
                phaseScoreUI.text = "Phase 1\n" + ScoreManager.instance.phaseScore[0].ToString();
            //if (Input.GetKey(KeyCode.Escape)) break;
            //t += Time.deltaTime;
            //Debug.Log(videoPlayer.time);
            yield return null;
        }
        //videoPlayer.enabled = false;
        //inGameUI.SetActive(true);
        //changePhaseCor = null;
    }
}
