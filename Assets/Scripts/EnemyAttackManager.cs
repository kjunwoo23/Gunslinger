using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public static EnemyAttackManager instance;

    public GameObject redDot;
    public Transform playerHeart;

    public float waitTime;
    public float minCoolTime;
    public float maxCoolTime;

    Coroutine redDotCor;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        redDotCor = StartCoroutine(BeforeRedDot());
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.instance.gameOver) return;

        if (redDotCor == null)
            redDotCor = StartCoroutine(RedDot());
    }

    public IEnumerator BeforeRedDot()
    {
        yield return new WaitForSeconds(3);
        redDotCor = null;
    }

    public IEnumerator RedDot()
    {
        GameObject tmp = Instantiate(redDot, playerHeart.position, Quaternion.Euler(0, 0, 0));

        EffectManager.instance.effectSounds[2].source.Play();
        yield return new WaitForSeconds(waitTime);



        EffectManager.instance.effectSounds[3].source.Play();
        if (tmp.GetComponent<RedDot>().hit)
            ScoreManager.instance.GameOver();
        Destroy(tmp);
        yield return new WaitForSeconds(Random.Range(minCoolTime, maxCoolTime));

        redDotCor = null;
    }
}
