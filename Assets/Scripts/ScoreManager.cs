using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;
    public int[] phaseScore = new int[3];

    public TextMeshProUGUI totalScoreText;
    public GameObject getScoreTextPrefab;

    public Rigidbody2D camRigid;
    public float camSpinPower;
    public TextMeshProUGUI gameOverText;

    public BoxCollider2D rightWall;
    public bool gameOver;

    public TextMeshProUGUI bulletUI;

    

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
        totalScoreText.text = "Score: " + score.ToString();
        if (PlayerShoot.instance.isRifle)
            bulletUI.text = PlayerShoot.instance.curRifleBullet + " / " + PlayerShoot.instance.maxRifleBullet;
        else
            bulletUI.text = PlayerShoot.instance.curRevolverBullet + " / " + PlayerShoot.instance.maxRevolverBullet;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

    }

    public void GetScoreUI(int score)
    {
        GameObject tmp = Instantiate(getScoreTextPrefab, PlayerShoot.instance.aimPos.position, Quaternion.Euler(0, 0, 0));
        tmp.GetComponent<GetScoreUI>().text.text = "+" + score.ToString();
    }

    public void GameOver()
    {
        if (CutSceneManager.instance.changePhaseCor != null) return;
        if (gameOver) return;
        gameOver = true;
        PlayerShoot.instance.StartCameraShake(7, 7);
        camRigid.angularVelocity = camSpinPower;
        gameOverText.enabled = true;
        rightWall.enabled = false;
        PlayerShoot.instance.noise.m_AmplitudeGain = 0;
        PlayerShoot.instance.noise.m_FrequencyGain = 0;
    }

    public void Restart()
    {
        PlayerPrefs.SetFloat("BgmTime", SoundManager.instance.bgmPlayer.time);
        PlayerPrefs.SetInt("Restart", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
