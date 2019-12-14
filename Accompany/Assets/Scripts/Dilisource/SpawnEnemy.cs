using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy Instance;

    [Header("敌人可能生成的位置")]
    [SerializeField]
    private Vector3[] spawnPointsRegionOne;//区域一敌人可能生成的位置
    [SerializeField]
    private Vector3[] spawnPointsRegionTwo;//区域二敌人可能生成的位置
    [SerializeField]
    private Vector3[] spawnPointsRegionThree;//区域三敌人可能生成的位置
    [SerializeField]
    private Vector3[] regionSplitPoints;

    private int regionNumber = 0;
    private Dictionary<int, Vector3[]> regionNumberToSpawnPointsNumber = new Dictionary<int, Vector3[]>();

    private Vector3 playerPosition;

    [SerializeField]
    private GameObject enemy;

    private float time;
    [SerializeField]
    private float intervalOfSpawnEnemy;
    private void Awake()
    {
        Instance = this;
        regionNumberToSpawnPointsNumber.Add(1, spawnPointsRegionOne);
        regionNumberToSpawnPointsNumber.Add(2, spawnPointsRegionTwo);
        regionNumberToSpawnPointsNumber.Add(3, spawnPointsRegionThree);
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

    }

    private void Update()
    {
        MonitorRegionNumber();
        if(regionNumber != 0)
        {
            time += Time.deltaTime;
            if(time > intervalOfSpawnEnemy)
            {
                InstantiateEnemy();
            }
        }
    }


    //上下阈值，中间有间隔


    //检测regionNumber值
    private void MonitorRegionNumber()
    {
        if(playerPosition.x >= regionSplitPoints[0].x && playerPosition.x <= regionSplitPoints[1].x)
        {
            regionNumber = 1;
        }
        else if(playerPosition.x >= regionSplitPoints[1].x && playerPosition.x <= regionSplitPoints[2].x)
        {
            regionNumber = 2;
        }
        else if (playerPosition.x >= regionSplitPoints[2].x && playerPosition.x <= regionSplitPoints[3].x)
        {
            regionNumber = 3;
        }
    }

    public void InstantiateEnemy()
    {
        Vector3[] tempPoints = regionNumberToSpawnPointsNumber[regionNumber];
        int minNumber = tempPoints.Length / 2;
        int maxNumber = tempPoints.Length - 1;
        GameObject[] nowEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(nowEnemys.Length >= maxNumber)
        {
            return;
        }
        else if(nowEnemys.Length >= minNumber)
        {
            //生成一个
            GameObject go = ObjectPool.Instance.GetEnemy();
            go.transform.position = tempPoints[Random.Range(0, tempPoints.Length)];
            //朝向
            if(playerPosition.x < go.transform.position.x)
            {
                go.transform.right *= -1;
            }
        }
        else if(nowEnemys.Length < minNumber)
        {
            //生成两个
            GameObject go = ObjectPool.Instance.GetEnemy();
            int temp = Random.Range(0, tempPoints.Length);
            go.transform.position = tempPoints[temp];
            //朝向
            if (playerPosition.x < go.transform.position.x)
            {
                go.transform.right *= -1;
            }
            //第二个
            go = ObjectPool.Instance.GetEnemy();
            go.transform.position = tempPoints[(temp + 2) % tempPoints.Length];
            if (playerPosition.x < go.transform.position.x)
            {
                go.transform.right *= -1;
            }
        }
        time = 0;
    }

}
