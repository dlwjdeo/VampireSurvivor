using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime = 0;
    public float maxGameTime = 2*10f;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 60, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PlayerController player;
    public PoolManager pool;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
