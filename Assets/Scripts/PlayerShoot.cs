using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    public bool isRifle;

    public Transform gunPos;
    public Transform aimPos;
    public LineRenderer line;
    public EdgeCollider2D edgeCollider;
    Vector2[] colliderPos = new Vector2[2];

    Vector3 clickPos;
    bool gunUsing;

    public GameObject rifleAim;
    public int maxRifleBullet;
    public int curRifleBullet;

    public GameObject revolverAim;
    public int maxRevolverBullet;
    public int curRevolverBullet;
    public GameObject blackDot;

    public Transform player;

    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin noise;
    public Transform camTarget;
    public Coroutine cameraShakeCor;
    public Coroutine constantCameraShakeCor;

    public TextMeshProUGUI cntText;
    public TextMeshProUGUI hitText;

    public List<EnemyMove> enemys = new List<EnemyMove>();

    public Transform gunUIPos;
    public Animator rifleUI, revolverUI;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        rifleUI.transform.position = gunUIPos.position;
        revolverUI.transform.position = gunUIPos.position;
        if (ScoreManager.instance.gameOver || PhaseManager.instance.cutScenePlaying)
        {
            if (line.enabled)
            {
                line.enabled = false;
                //aim.gameObject.SetActive(false);
                rifleAim.SetActive(false);
                //cntText.enabled = false;
                //hitText.enabled = false;
            }
            if (revolverAim.activeSelf)
                revolverAim.SetActive(false);
            return;
        }

        if (Time.timeScale == 0) return;

        clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimPos.position = clickPos + new Vector3(0, 0, 20);

        if (curRifleBullet == 0 && !PhaseManager.instance.reverse)
            if (CutSceneManager.instance.changePhaseCor == null)
            {
                CutSceneManager.instance.StartToPhase2();
            }
        if (curRevolverBullet == 0)
            if (CutSceneManager.instance.changePhaseCor == null)
            {
                CutSceneManager.instance.StartToPhase3();
            }
        if (curRifleBullet == 0 && PhaseManager.instance.reverse)
            if (CutSceneManager.instance.changePhaseCor == null)
            {
                CutSceneManager.instance.StartGameClear();
            }


        if (isRifle)
        {
            line.SetPosition(0, aimPos.position);
            line.SetPosition(1, gunPos.position);

            colliderPos[0] = new Vector2(0, 0);
            colliderPos[1] = new Vector2(gunPos.position.x - aimPos.position.x, gunPos.position.y - aimPos.position.y);
            edgeCollider.points = colliderPos;

            cntText.text = enemys.Count.ToString();

            if (Input.GetKeyDown(KeyCode.Mouse0) && curRifleBullet > 0 && !gunUsing)
            {
                gunUsing = true;
                StartCoroutine(ShootRifle());
            }
        }
        else
        {
            float tmpDistance = Vector2.Distance(gunPos.position, aimPos.position);
            revolverAim.transform.localScale = new Vector3(0.5f + tmpDistance * 0.5f, 0.5f + tmpDistance * 0.5f, 1);
            //원 범위의 랜덤한 곳에 raycast를 쏘고, 적 히트박스에 맞으면 제거
            if (Input.GetKeyDown(KeyCode.Mouse0) && curRevolverBullet > 0 && !gunUsing)
            {
                gunUsing = true;
                StartCoroutine(ShootRevolver());
            }
        }
    }

    public IEnumerator ShootRifle()
    {
        StartCameraShake(5, 2);
        EffectManager.instance.effectSounds[0].source.Play();

        //Debug.Log(enemys.Count);
        curRifleBullet--;
        ScoreManager.instance.GetScoreUI(enemys.Count * enemys.Count * 100);
        ScoreManager.instance.score += enemys.Count * enemys.Count * 100;
        for (int i = enemys.Count - 1; i >= 0; i--)
            try
            {
                if (enemys[i].enabled)
                    enemys[i].Die();
            }
            catch
            {
                //Debug.Log("error");
            }
        enemys.Clear();
        rifleUI.SetTrigger("shoot");
        yield return new WaitForSeconds(0.7f);
        EffectManager.instance.effectSounds[1].source.Play();
        yield return new WaitForSeconds(0.7f);
        gunUsing = false;
    }
    public IEnumerator ShootRevolver()
    {
        StartCameraShake(3, 2);
        EffectManager.instance.effectSounds[0].source.Play();

        //Debug.Log(enemys.Count);
        curRevolverBullet--;

        Vector3 shootPos = Random.onUnitSphere * revolverAim.transform.localScale.x * 0.15f;
        //Debug.Log(shootPos.x / revolverAim.transform.localScale.x * 4 + ", " + shootPos.y / revolverAim.transform.localScale.x * 4 + ", " + shootPos.z / revolverAim.transform.localScale.x * 4);
        GameObject black = Instantiate(blackDot, transform.position + shootPos, Quaternion.Euler(0, 0, 0));


        revolverUI.SetTrigger("shoot");
        yield return new WaitForSeconds(0.5f);
        Destroy(black);

        //yield return new WaitForSeconds(0.6f);
        EffectManager.instance.effectSounds[1].source.Play();
        yield return new WaitForSeconds(0.5f);
        gunUsing = false;
    }


    public void StartCameraShake(float strength, float duration)
    {
        if (cameraShakeCor != null)
            StopCoroutine(cameraShakeCor);
        camTarget.localPosition = new Vector3(0, 0, 0);
        //camTarget.position = player.position;
        cameraShakeCor = StartCoroutine(CameraShake(strength, duration));
    }

    public IEnumerator CameraShake(float strength, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            camTarget.position -= new Vector3(0, strength, 0);
            yield return new WaitForSeconds(0.1f);
            camTarget.position += new Vector3(0, 2 * strength, 0);
            yield return new WaitForSeconds(0.2f);
            camTarget.position -= new Vector3(0, strength, 0);
            yield return new WaitForSeconds(0.1f);
            strength *= 0.5f;
        }
        camTarget.localPosition = new Vector3(0, 0, 0);
        //camTarget.position = player.position;
        cameraShakeCor = null;
    }
    public void StartConstantCameraShake(float strength, float rate)
    {
        if (constantCameraShakeCor != null)
            StopCoroutine(constantCameraShakeCor);
        camTarget.localPosition = new Vector3(0, 0, 0);
        //camTarget.position = player.position;
        constantCameraShakeCor = StartCoroutine(ConstantCameraShake(strength, rate));
    }

    public IEnumerator ConstantCameraShake(float strength, float rate)
    {
        while (true)
        {
            if (ScoreManager.instance.gameOver)
            {
                constantCameraShakeCor = null;
                yield break;
            }
            StartCameraShake(strength, 1);
            yield return new WaitForSeconds(rate);
        }
    }
    public void StopConstantCameraShake()
    {
        if (constantCameraShakeCor != null)
            StopCoroutine(constantCameraShakeCor);
        camTarget.localPosition = new Vector3(0, 0, 0);
        constantCameraShakeCor = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRifle)
            if (collision.CompareTag("EnemyHitbox"))
            {
                enemys.Add(collision.transform.parent.GetComponent<EnemyMove>());
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isRifle)
            if (collision.CompareTag("EnemyHitbox"))
            {
                enemys.Remove(collision.transform.parent.GetComponent<EnemyMove>());
            }
    }
}
