using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTruck : EnemyMove
{
    public bool explode;
    public float explodeSpeed;
    Coroutine explodeCor;
    
    public override void Die()
    {
        if (explodeCor == null)
            explodeCor = StartCoroutine(DieCor(1.5f));
    }

    IEnumerator DieCor(float t)
    {
        gotShot = true;
        float maxT = t;
        explode = true;
        while (t > 0.5f)
        {
            t -= Time.fixedDeltaTime;
            transform.position -= new Vector3((maxT - t) / maxT * explodeSpeed * Time.fixedDeltaTime, 0, 0);
            yield return new WaitForFixedUpdate();
        }
        animator.SetTrigger("bomb");
        PlayerShoot.instance.StartCameraShake(7, 2);
        EffectManager.instance.effectSounds[4].source.Play();
        while (t > 0)
        {
            t -= Time.fixedDeltaTime;
            transform.position -= new Vector3((maxT - t) / maxT * explodeSpeed * Time.fixedDeltaTime, 0, 0);
            yield return new WaitForFixedUpdate();
        }
        explode = false;
        base.Die();
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        if (explode) return;
        base.OnCollisionStay2D(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (explode)
            if (collision.CompareTag("PlayerHitbox"))
                ScoreManager.instance.GameOver();
    }
}
