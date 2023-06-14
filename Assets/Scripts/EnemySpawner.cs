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

    private float spawnYOffSetFromPlayer = 30f;

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

        appliedSpawnInterval = UnityEngine.Random.Range(minSpawnInterval, maxSpawnInterval);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerGo.transform.position + new Vector3(0,spawnYOffSetFromPlayer,0);

        elapsedTime += Time.deltaTime;
        TrySpawnEnemies();
    }

    private void TrySpawnEnemies()
    {
        if(elapsedTime >= appliedSpawnInterval) {
            elapsedTime = 0f;
            int randomNumber = UnityEngine.Random.Range(0, enemyPrefabs.Count);
            appliedSpawnInterval = UnityEngine.Random.Range(minSpawnInterval, maxSpawnInterval);
            Debug.Log("appliedSpawnInterval: " + appliedSpawnInterval);
            GameObject instantiatedEnemy = Instantiate(enemyPrefabs[randomNumber]);

            Debug.Log("Spawning enemy" + instantiatedEnemy.name);
            instantiatedEnemy.transform.position = this.transform.position;
        }
    }
}
