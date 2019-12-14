using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : MonoBehaviour
{
    private Vector3 playerPosition;

    [Header("狗和人的最小距离")]
    [Range(0, 5)]
    [SerializeField]
    private float minDistance;
    [Header("狗移动到快接近人时的缓冲距离")]
    [Range(0, 1.5f)]
    [SerializeField]
    private float moveBuffer;
    [Header("狗靠近人所需要的大致时间")]
    [Range(0, 2)]
    [SerializeField]
    private float smoothTime;


    [SerializeField]
    private float coefficient;

    [SerializeField]
    private float actionTime;//狗反应时间
    private float time;
    private float flag = 0;
    public Vector3 currentVelocity = Vector3.zero;
    private Vector3 targetPos;
 
    private void Awake()
    {
        //TODO
        //找到player的Transform组件
        //playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }
    private void Update()
    {
        //if(人物开始行走)
        //{
        //    time += Time.deltaTimel; 
        //}
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
    }

    private void LateUpdate()
    {
        //if(time > actionTime)
        if (Vector3.Distance(transform.position, targetPos//注意要改成playerPosition
            ) >= minDistance && Vector3.Distance(transform.position, targetPos) <= minDistance + moveBuffer)
        {
            Debug.Log("1st");
            if (flag == 0)
            {
                FollowWithPlayer();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, coefficient);
                if (Vector3.Distance(transform.position, targetPos) < 0.05f)
                {
                    flag = 0;
                }
            }
        }
        else if (Vector3.Distance(transform.position, targetPos//注意要改成playerPosition
            ) > minDistance + moveBuffer)
        {
            Debug.Log("2nd");
            FollowWithPlayer();
            flag = 1;
        }
    }

    private void FollowWithPlayer()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
    }
}
