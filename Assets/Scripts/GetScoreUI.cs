using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FadeOut()
    {
        text.color = new Color(0, 0, 0, 1);
        while(time > 0)
        {
            text.color -= new Color(0, 0, 0, Time.deltaTime / time);
            time -= Time.deltaTime;
            yield return null;
        }
        text.color *= 0;
        Destroy(gameObject);              
    }
}
