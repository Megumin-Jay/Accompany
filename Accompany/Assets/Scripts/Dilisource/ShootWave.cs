using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWave : MonoBehaviour
{
    [Header("发射一次攻击产生的声波数量")]
    [SerializeField]
    private int numberOfWave;

    [Range(0, 5)]
    [SerializeField]
    private float intervalOfShoot;

    [Range(0,1)]
    [SerializeField]
    private float intervalOfWave;//每一次射击，上一个声波与下一个声波产生间隔

    private float time;//计时器
    private void Update()
    {
        time += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && time > intervalOfShoot)
        {
            time = 0;
            StartCoroutine(ShootSoundWave());
        }
    }

    private IEnumerator ShootSoundWave()
    {
        for(int i = 0; i < numberOfWave; i++)
        {
            GameObject go = ObjectPool.Instance.GetSoundWave();
            //TODO
            //生成的位置与小女孩朝向相关
            
            yield return new WaitForSeconds(intervalOfWave);
        }
    }

}
