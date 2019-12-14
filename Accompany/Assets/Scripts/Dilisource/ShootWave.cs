using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//绑定在角色身上
public class ShootWave : MonoBehaviour
{
    [Header("发射一次攻击产生的声波数量")]
    [SerializeField]
    private int numberOfWave;

    [Range(0, 5)]
    [SerializeField]
    private float intervalOfShoot;

    [Range(0, 1)]
    [SerializeField]
    private float intervalOfWave;//每一次射击，上一个声波与下一个声波产生间隔

    private float time;//计时器
    private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();

    }
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
        string nowAnimationName = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        Vector3 dir = Vector3.right;
        if(nowAnimationName.Contains("Right"))
        {
            dir = Vector3.right;
        }
        else if(nowAnimationName.Contains("Down"))
        {
            dir = Vector3.down;
        }
        else if (nowAnimationName.Contains("Left"))
        {
            dir = Vector3.left;
        }
        else if (nowAnimationName.Contains("Up"))
        {
            dir = Vector3.up;
        }
        for (int i = 0; i < numberOfWave; i++)
        {
            GameObject go = ObjectPool.Instance.GetSoundWave();
            //TODO
            //生成的位置与小女孩朝向相关
            //go.transform.position = transform.position + dir*0.5f;//是否需要一个系数
            go.transform.position = transform.position;//是否需要一个系数
            go.transform.right = dir;
            go.GetComponent<Rigidbody2D>().velocity = dir;//要不要加一个系数
            yield return new WaitForSeconds(intervalOfWave);
        }
    }

}
