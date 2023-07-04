using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDot : MonoBehaviour
{
    public bool hit;
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
        tmpX = Mathf.Lerp(transform.position.x, EnemyAttackManager.instance.playerHeart.transform.position.x, 0.05f);
        tmpY = Mathf.Lerp(transform.position.y, EnemyAttackManager.instance.playerHeart.transform.position.y, 0.05f);
        transform.position = new Vector3(tmpX, tmpY, 0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
            hit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
            hit = false;
    }
}
