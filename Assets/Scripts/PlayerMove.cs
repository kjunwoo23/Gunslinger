using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

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
        if (ScoreManager.instance.gameOver)
        {
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
        moveSpeed *= 2;
        yield return new WaitForSeconds(rushTime);
        moveSpeed *= 0.5f;
        yield return new WaitForSeconds(rushCool);

        rushCor = null;
    }
}
