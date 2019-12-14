using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy Instance;

    //
    [Header("敌人可能生成的位置")]
    [SerializeField]
    private Transform[] spawnPointsRegionOne;//区域一敌人可能生成的位置

    [SerializeField]
    private Transform[] spawnPointsRegionTwo;//区域二敌人可能生成的位置
    [SerializeField]
    private Transform[] spawnPointsRegionThree;//区域三敌人可能生成的位置
    //
    [SerializeField]
    private Transform[] regionSplitPoints;

    private int regionNumber = 0;
    private Dictionary<int, Transform[]> regionNumberToSpawnPointsNumber = new Dictionary<int, Transform[]>();

    private Transform playerTransform;
    private int minNumber;
    private int maxNumber;
    [SerializeField]
    private GameObject enemy;

    private float time;

    private float intervalOfSpawnEnemy;


    private void Awake()
    {
        Instance = this;
        regionNumberToSpawnPointsNumber.Add(1, spawnPointsRegionOne);
        regionNumberToSpawnPointsNumber.Add(2, spawnPointsRegionTwo);
        regionNumberToSpawnPointsNumber.Add(3, spawnPointsRegionThree);
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        MonitorRegionNumber();
        if(regionNumber != 0 && regionNumber != 4)
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
        if(playerTransform.position.x >= regionSplitPoints[0].position.x && playerTransform.position.x <= regionSplitPoints[1].position.x)
        {
            regionNumber = 1;
            minNumber = 2;
            maxNumber = 5;
            intervalOfSpawnEnemy = 2f;
        }
        else if(playerTransform.position.x >= regionSplitPoints[1].position.x && playerTransform.position.x <= regionSplitPoints[2].position.x)
        {
            regionNumber = 2;
            minNumber = 2;
            maxNumber = 4;
            intervalOfSpawnEnemy = 1.5f;
        }
        else if (playerTransform.position.x >= regionSplitPoints[2].position.x && playerTransform.position.x <= regionSplitPoints[3].position.x)
        {
            regionNumber = 3;
            minNumber = 1;
            maxNumber = 3;
            intervalOfSpawnEnemy = 1.5f;
        }
        else if (playerTransform.position.x >= regionSplitPoints[3].position.x)
        {
            regionNumber = 4;
        }

    }

    public void InstantiateEnemy()
    {
        //Transform[] tempPoints = regionNumberToSpawnPointsNumber[regionNumber];
        Transform[] tempPoints = spawnPointsRegionOne;
        GameObject[] nowEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(nowEnemys.Length >= maxNumber)
        {
            return;
        }
        else if(nowEnemys.Length >= minNumber)
        {
            //生成一个
            GameObject go = ObjectPool.Instance.GetEnemy();
            go.transform.position = tempPoints[Random.Range(0, tempPoints.Length)].position;
            //朝向
            if(playerTransform.position.x < go.transform.position.x)
            {
                go.transform.right *= -1;
            }
        }
        else if(nowEnemys.Length < minNumber)
        {
            //生成两个
            GameObject go = ObjectPool.Instance.GetEnemy();
            int temp = Random.Range(0, tempPoints.Length);
            go.transform.position = tempPoints[temp].position;
            //朝向
            if (playerTransform.position.x < go.transform.position.x)
            {
                go.transform.right *= -1;
            }
            //第二个
            go = ObjectPool.Instance.GetEnemy();
            go.transform.position = tempPoints[(temp + 2) % tempPoints.Length].position;
            if (playerTransform.position.x < go.transform.position.x)
            {
                go.transform.right *= -1;
            }
        }
        time = 0;
    }

}
