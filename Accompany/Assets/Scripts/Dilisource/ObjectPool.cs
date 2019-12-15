using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("声波预制体")]
    [SerializeField]
    private GameObject soundWave;
    private List<GameObject> soundWaveList = new List<GameObject>();
    private const int soundWaveCount = 5;

    [Header("敌人预制体")]
    [SerializeField]
    private GameObject enemy;
    private List<GameObject> enemyList = new List<GameObject>();
    private const int enemyNumber = 5;
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        Instance = this;
        InitSoundWave();
        InitEnemy();
    }
    #region 声波
    private void InitSoundWave()
    {
        for (int i = 0; i < soundWaveCount; i++)
        {
            GameObject go = InstantiateSoundWave();
            go.SetActive(false);
        }
    }
    private GameObject InstantiateSoundWave()
    {
        GameObject go = Instantiate(soundWave, transform);
        soundWaveList.Add(go);
        return go;
    }
    public GameObject GetSoundWave()
    {
        GameObject temp;
        for(int i = 0; i < soundWaveList.Count; i++)
        {
            temp = soundWaveList[i];
            if(temp.activeInHierarchy == false)
            {
                temp.SetActive(true);
                return temp;
            }
        }
        return InstantiateSoundWave();
    }
    #endregion

    #region 敌人
    private GameObject InstantiateEnemy()
    {
        GameObject go = Instantiate(enemy, transform);
        enemyList.Add(go);
        return go;
    }
    private void InitEnemy()
    {
        for(int i = 0; i < enemyNumber; i++)
        {
            GameObject go = InstantiateEnemy();
            go.SetActive(false);
        }
    }
    public GameObject GetEnemy()
    {
        GameObject temp;
        for(int i = 0; i < enemyList.Count; i++)
        {
            temp = enemyList[i];
            if(temp.activeInHierarchy == false)
            {
                temp.SetActive(true);
                return temp;
            }
        }
        return InstantiateEnemy();
    }
    #endregion
}
