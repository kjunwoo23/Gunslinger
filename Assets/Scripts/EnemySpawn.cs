using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyTruckPrefab;
    public GameObject enemyGatlingPrefab;

    public Transform[] spawnXRange = new Transform[2];
    public Transform[] spawnYRange = new Transform[2];

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(int spawnNum)
    {
        for (int i = 0; i < spawnNum; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(spawnXRange[0].position.x, spawnXRange[1].position.x), Random.Range(spawnYRange[0].position.y, spawnYRange[1].position.y), 0);

            int randType = Random.Range(0, 20);
            switch (randType)
            {
                case 0: Instantiate(enemyTruckPrefab, randPos, Quaternion.Euler(0, 0, 0)); break;
                case 1: Instantiate(enemyGatlingPrefab, randPos, Quaternion.Euler(0, 0, 0)); break;
                default: Instantiate(enemyPrefab, randPos, Quaternion.Euler(0, 0, 0)); break;
            }
        }

    }
}
