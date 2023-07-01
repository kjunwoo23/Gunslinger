using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;

    public TextMeshProUGUI totalScoreText;
    public GameObject getScoreTextPrefab;

    public Rigidbody2D camRigid;
    public float camSpinPower;
    public TextMeshProUGUI gameOverText;

    public BoxCollider2D rightWall;
    public bool gameOver;

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
    }

    public void GetScoreUI(int score)
    {
        GameObject tmp = Instantiate(getScoreTextPrefab, PlayerShoot.instance.aim.transform.position, Quaternion.Euler(0, 0, 0));
        tmp.GetComponent<GetScoreUI>().text.text = "+" + score.ToString();
    }

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;
        PlayerShoot.instance.StartCameraShake(7, 7);
        camRigid.angularVelocity = camSpinPower;
        gameOverText.enabled = true;
        rightWall.enabled = false;
        PlayerShoot.instance.noise.m_AmplitudeGain = 0;
        PlayerShoot.instance.noise.m_FrequencyGain = 0;
    }
}
