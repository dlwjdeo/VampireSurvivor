using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData; 

    private int level;
    private float timer;

    private void Awake()
    {
        //spawnPoint[0] 자식이 아닌 자신이기 때문에 자식을 사용하기 위해서는 1부터 사용
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min( Mathf.FloorToInt( GameManager.instance.gameTime / 10f), spawnData.Length - 1);
        if(timer > spawnData[level].spawnTime)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
        enemy.GetComponent<EnemyController>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
