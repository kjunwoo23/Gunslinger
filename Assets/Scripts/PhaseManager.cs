using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;

    public PlayerMove player;
    public int phase;
    public bool reverse;
    public bool cutScenePlaying;

    PlayerShoot playerShoot;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerShoot = PlayerShoot.instance;
        phase = 1;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeToPhase1();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeToPhase2();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeToPhase3();*/
        if (reverse)
            if (PlayerShoot.instance.constantCameraShakeCor == null && PlayerShoot.instance.cameraShakeCor == null && !ScoreManager.instance.gameOver)
                PlayerShoot.instance.StartConstantCameraShake(1, 0.3f);
    }

    public void ChangeToPhase1()
    {
        PlayerShoot.instance.StopConstantCameraShake();
        player.transform.localScale = new Vector3(1, 1, 1);
        EnemySpawn.instance.ClearEnemy();
        playerShoot.line.enabled = true;
        playerShoot.curRifleBullet = 6;
        playerShoot.isRifle = true;
        playerShoot.rifleAim.SetActive(true);
        playerShoot.revolverAim.SetActive(false);
        EnemySpawn.instance.SpawnEnemy(50);
        EnemyAttackManager.instance.Wait();
        cutScenePlaying = false;
    }
    public void ChangeToPhase2()
    {
        PlayerShoot.instance.StopConstantCameraShake();
        player.transform.localScale = new Vector3(1, 1, 1);
        EnemySpawn.instance.ClearEnemy();
        playerShoot.line.enabled = false;
        playerShoot.curRevolverBullet = 6;
        playerShoot.isRifle = false;
        playerShoot.rifleAim.SetActive(false);
        playerShoot.revolverAim.SetActive(true);
        EnemySpawn.instance.SpawnEnemy(50);
        EnemyAttackManager.instance.Wait();
        cutScenePlaying = false;
    }
    public void ChangeToPhase3()
    {
        PlayerShoot.instance.StartConstantCameraShake(1, 0.3f);
        reverse = true;
        player.transform.localScale = new Vector3(-1, 1, 1);
        EnemySpawn.instance.ClearEnemy();
        playerShoot.line.enabled = true;
        playerShoot.curRifleBullet = 6;
        playerShoot.isRifle = true;
        playerShoot.rifleAim.SetActive(true);
        playerShoot.revolverAim.SetActive(false);
        EnemySpawn.instance.SpawnEnemy(50);
        EnemyAttackManager.instance.Wait();
        cutScenePlaying = false;
    }
}
