using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDot : MonoBehaviour
{
    public bool hit;
    public bool enemyHit;
    public bool isRed;
    public bool isBlack;
    public bool isGrey;
    float tmpX, tmpY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isBlack || isGrey) return;

        tmpX = Mathf.Lerp(transform.position.x, EnemyAttackManager.instance.playerHeart.transform.position.x, 0.05f);
        tmpY = Mathf.Lerp(transform.position.y, EnemyAttackManager.instance.playerHeart.transform.position.y, 0.05f);
        transform.position = new Vector3(tmpX, tmpY, 0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isRed)
        {
            if (collision.CompareTag("PlayerHitbox"))
                hit = true;
        }
        else if (isGrey)
        {
            if ((collision.CompareTag("Enemy") || collision.CompareTag("Truck") || collision.CompareTag("Gatling")) && !enemyHit)
            {
                if (collision.GetComponent<EnemyMove>().gotShot) return;
                enemyHit = true;
                collision.GetComponent<EnemyMove>().gotShot = true;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                ScoreManager.instance.GetScoreUI(100);
                ScoreManager.instance.score += 100;
                collision.gameObject.GetComponent<EnemyMove>().Die();
            }
            if (collision.CompareTag("PlayerHitbox"))
                ScoreManager.instance.GameOver();
        }
        else if (collision.CompareTag("EnemyHitbox") && !enemyHit)
        {
            enemyHit = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            ScoreManager.instance.GetScoreUI(Mathf.RoundToInt(Vector3.Distance(PlayerShoot.instance.gunPos.position, transform.position) * Vector3.Distance(PlayerShoot.instance.gunPos.position, transform.position) * 20));
            ScoreManager.instance.score += Mathf.RoundToInt(Vector3.Distance(PlayerShoot.instance.gunPos.position, transform.position) * Vector3.Distance(PlayerShoot.instance.gunPos.position, transform.position) * 20);
            collision.transform.parent.gameObject.GetComponent<EnemyMove>().Die();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isBlack)
            if (collision.CompareTag("PlayerHitbox"))
                hit = false;
    }
}
