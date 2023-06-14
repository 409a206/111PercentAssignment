using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //시스템 조정 변수
    //스폰 최소 시간 간격
    private float minSpawnInterval = 2.0f;
    //스폰 최대 시간 간격
    private float maxSpawnInterval = 5.0f;
    private float appliedSpawnInterval;

    private float elapsedTime = 0f;

    private GameObject playerGo;
    // Start is called before the first frame update
    private string enemyPrefabPath = "Prefabs/Enemy/";

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    void Start()
    {
        playerGo = GameObject.Find("Player");

        foreach (GameObject loadedPrefab in Resources.LoadAll(enemyPrefabPath))
        {
            enemyPrefabs.Add(loadedPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerGo.transform.position + new Vector3(0,30,0);
        TrySpawnEnemies();
    }

    private void TrySpawnEnemies()
    {
        int randomNumber = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        appliedSpawnInterval = UnityEngine.Random.Range(minSpawnInterval, maxSpawnInterval);

        GameObject instantiatedEnemy = Instantiate(enemyPrefabs[randomNumber]);
        instantiatedEnemy.transform.position = this.transform.position;
    }
}
