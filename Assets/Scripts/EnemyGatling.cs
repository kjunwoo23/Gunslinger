using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGatling : EnemyMove
{
    public GameObject greyDot;
    public bool gatling;

    public float minRange;
    public float maxRange;

    Coroutine gatlingCor;
    public override void FixedUpdate()
    {
        if (ScoreManager.instance.gameOver && !gatling)
        {
            transform.position += new Vector3(moveSpeed * 30 * Time.fixedDeltaTime, 0, 0);
        }
        else if (gatling)
        {
            transform.position -= new Vector3(0.5f * Time.fixedDeltaTime, 0, 0);
        }
    }
    public override void Die()
    {
        if (gatlingCor == null)
            gatlingCor = StartCoroutine(DieCor());
    }

    IEnumerator DieCor()
    {
        gotShot = true;
        gatling = true;

        float angle = 0;
        EffectManager.instance.effectSounds[5].source.Play();

        PlayerShoot.instance.StartConstantCameraShake(3, 0.3f);
        while (angle <= 360)
        {
            GameObject grey = Instantiate(greyDot, transform.position + new Vector3(-Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * Random.Range(minRange, maxRange), Quaternion.Euler(0, 0, 0));
            StartCoroutine(DisappearGrey(grey));
            yield return new WaitForSeconds(0.05f);
            angle += 10;
        }
        PlayerShoot.instance.StopConstantCameraShake();

        animator.SetTrigger("bomb");
        PlayerShoot.instance.StartCameraShake(7, 2);
        EffectManager.instance.effectSounds[4].source.Play();

        yield return new WaitForSeconds(1.5f);

        gatling = false;
        base.Die();
    }

    IEnumerator DisappearGrey(GameObject grey)
    {
        if (PhaseManager.instance.reverse)
            yield return new WaitForSeconds(0.1f);
        else
            yield return new WaitForSeconds(0.5f);
        Destroy(grey);
    }
}
