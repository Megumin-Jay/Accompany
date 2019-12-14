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
    private void Awake()
    {
        Instance = this;
        InitSoundWave();
    }
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

}
