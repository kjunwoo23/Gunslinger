using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDot : MonoBehaviour
{
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
