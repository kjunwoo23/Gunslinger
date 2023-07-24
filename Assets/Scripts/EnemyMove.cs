using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed;
    float reDirCnt;

    public bool gotShot;

    public Animator animator;

    //public bool aimIn;
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(ToyRandMove(true));
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (ScoreManager.instance.gameOver)
        {
            transform.position += new Vector3(moveSpeed * 30 * Time.fixedDeltaTime, 0, 0);
        }
    }
    public IEnumerator ToyRandMove(bool infinite)
    {
        reDirCnt = 0;
        Vector3 dir = new Vector3(0, 0, 0);
        while (infinite)
        {
                if (reDirCnt < 0)
                {
                    dir = new Vector3(Random.Range(-1f, 1f) * Random.Range(0.5f * moveSpeed, moveSpeed), Random.Range(-1f, 1f) * Random.Range(0.5f * moveSpeed, moveSpeed), 0);
                    reDirCnt = Random.Range(0, 3f);
                }
                //rigidBody.velocity = new Vector3(dir.x, dir.y, 0);
                transform.position += dir * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            reDirCnt -= Time.fixedDeltaTime;

            if (ScoreManager.instance.gameOver) yield break;
        }
    }

    public virtual void Die()
    {
        gotShot = true;
        if (gameObject)
            Destroy(gameObject);
        else Debug.Log(1);
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(1);
        if (collision.gameObject.CompareTag("Wall"))
        {
            reDirCnt = 0;
        }
        if (collision.gameObject.GetComponent<EnemyTruck>())
            if (collision.gameObject.GetComponent<EnemyTruck>().explode)
            {
                if (gotShot) return;
                //Debug.Log(2);
                ScoreManager.instance.GetScoreUI(100);
                ScoreManager.instance.score += 100;
                Die();
            }
    }
}
