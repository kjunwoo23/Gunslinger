using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public MeshRenderer quadRenderer;
    public float scrollSpeed;
    //public float minSpeed;

    int dir;

    //Player player;
    float visionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        quadRenderer.sortingOrder = -1000;
        dir = 1;
        //player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //if (player == null)
        //    player = Player.instance;

        /*if (DialogueManager.instance.talkable)
        {
            if (visionSpeed > minSpeed)
                visionSpeed -= visionSpeed * Time.deltaTime;
            if (visionSpeed < minSpeed)
                visionSpeed = minSpeed;
        }
        else
        {
            visionSpeed = 1 + (player.maxVision - player.curVision) / player.maxVision * 50;
            transform.position = new Vector3(player.transform.position.x, 0, 1);
        }*/

        if (ScoreManager.instance.gameOver) return;

        if (PhaseManager.instance.reverse) dir = -1;
        else dir = 1;
        Vector2 textureOffset = new Vector2(quadRenderer.material.mainTextureOffset.x + dir * scrollSpeed * Time.deltaTime * 0.01f, 0);
        quadRenderer.material.mainTextureOffset = textureOffset;
    }
}
