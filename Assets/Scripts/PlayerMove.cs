using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

    public SpriteRenderer sprite;
    public ParticleSystem particle;
    public Image rushUI;
    public float rushTime;
    public float rushCool;
    Coroutine rushCor;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        sprite.sortingOrder = (int)(-transform.position.y);
        
        if (ScoreManager.instance.gameOver)
        {
            particle.Stop();
            if (animator.enabled) animator.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            if (rushCor == null)
                rushCor = StartCoroutine(Rush());
    }

    void FixedUpdate()
    {
        if (ScoreManager.instance.gameOver) return;

        if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0);
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, moveSpeed * Time.fixedDeltaTime, 0);
        if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0, moveSpeed * Time.fixedDeltaTime, 0);
    }

    IEnumerator Rush()
    {
        float tmp;
        moveSpeed *= 2;
        rushUI.fillMethod = Image.FillMethod.Vertical;
        for (tmp = 0; tmp < rushTime; tmp += Time.deltaTime)
        {
            rushUI.fillAmount = (rushTime - tmp) / rushTime;
            yield return null;
        }

        moveSpeed *= 0.5f;
        rushUI.fillMethod = Image.FillMethod.Radial360;
        for (tmp = 0; tmp < rushCool; tmp += Time.deltaTime)
        {
            rushUI.fillAmount = tmp / rushCool;
            yield return null;
        }
        rushUI.fillAmount = 1;
        rushCor = null;
    }
}
