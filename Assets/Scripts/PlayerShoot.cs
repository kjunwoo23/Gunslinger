using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    public Transform gunPos;
    public SpriteRenderer aim;
    public LineRenderer line;
    public EdgeCollider2D edgeCollider;
    Vector2[] colliderPos = new Vector2[2];

    Vector3 clickPos;
    bool gunUsing;

    public Transform player;

    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin noise;
    public Transform camTarget;
    Coroutine cameraShakeCor;

    public TextMeshProUGUI cntText;
    public TextMeshProUGUI hitText;

    public List<EnemyMove> enemys = new List<EnemyMove>();


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
        if (ScoreManager.instance.gameOver)
        {
            if (line.enabled)
            {
                line.enabled = false;
                //aim.gameObject.SetActive(false);
                aim.enabled = false;
                cntText.enabled = false;
                hitText.enabled = false;
            }
            return;
        }

        clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aim.transform.position = clickPos + new Vector3(0, 0, 20);
        line.SetPosition(0, aim.transform.position);
        line.SetPosition(1, gunPos.position);

        colliderPos[0] = new Vector2(0, 0);
        colliderPos[1] = new Vector2(gunPos.position.x - aim.transform.position.x, gunPos.position.y - aim.transform.position.y);
        edgeCollider.points = colliderPos;

        cntText.text = enemys.Count.ToString();

        if (Input.GetKeyDown(KeyCode.Mouse0) && ScoreManager.instance.curRifleBullet > 0 && !gunUsing)
        {
            gunUsing = true;
            StartCoroutine(ShootRifle());
        }
    }

    public IEnumerator ShootRifle()
    {
        StartCameraShake(5, 2);
        EffectManager.instance.effectSounds[0].source.Play();

        //Debug.Log(enemys.Count);
        ScoreManager.instance.curRifleBullet--;
        ScoreManager.instance.GetScoreUI(enemys.Count * enemys.Count * 100);
        ScoreManager.instance.score += enemys.Count * enemys.Count * 100;
        for (int i = enemys.Count - 1; i >= 0; i--)
            Destroy(enemys[i].gameObject);
        enemys.Clear();

        yield return new WaitForSeconds(0.7f);
        EffectManager.instance.effectSounds[1].source.Play();
        yield return new WaitForSeconds(0.7f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHitbox"))
        {
            enemys.Add(collision.transform.parent.GetComponent<EnemyMove>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHitbox"))
        {
            enemys.Remove(collision.transform.parent.GetComponent<EnemyMove>());
        }
    }
}
