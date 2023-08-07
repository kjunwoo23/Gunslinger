using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

    public Transform enemyPool;

    public GameObject enemyPrefab;
    public GameObject enemyTruckPrefab;
    public GameObject enemyGatlingPrefab;

    public Transform[] spawnXRange = new Transform[2];
    public Transform[] spawnYRange = new Transform[2];

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //PhaseManager.instance.ChangeToPhase1();
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
                case 0: Instantiate(enemyTruckPrefab, randPos, Quaternion.Euler(0, 0, 0), enemyPool); break;
                case 1: Instantiate(enemyGatlingPrefab, randPos, Quaternion.Euler(0, 0, 0), enemyPool); break;
                default: Instantiate(enemyPrefab, randPos, Quaternion.Euler(0, 0, 0), enemyPool); break;
            }
        }

    }

    public void ClearEnemy()
    {
        // child 에는 부모와 자식이 함께 설정 된다.
        var child = enemyPool.GetComponentsInChildren<EnemyMove>();

        foreach (var iter in child)
        {
            // 부모(this.gameObject)는 삭제 하지 않기 위한 처리
            if (iter != enemyPool.transform)
            {
                Destroy(iter.gameObject);
            }
        }
    }
}
