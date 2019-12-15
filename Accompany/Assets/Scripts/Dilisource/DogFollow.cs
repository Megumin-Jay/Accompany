using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : MonoBehaviour
{
    private Transform playerTransform;
    private Animator playerAnimator;
    private Animator dogAnimator;

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
    private Vector3 targetPos;


    [SerializeField]
    private float coefficient;

    [SerializeField]
    private float actionTime;//狗反应时间
    private float time;
    private float flag = 0;
    [SerializeField]
    private float leaveSpeed;

    private Vector3 currentVelocity = Vector3.zero;
 
    private enum DogStatus
    {
        Follow,Leave
    }
    private DogStatus dogStatus = DogStatus.Follow;
    private void Awake()
    {
        //TODO
        //找到player的Transform组件
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        dogAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Walk"))
        {
            time += Time.deltaTime;
        }
        if(playerTransform.transform.position.x > 21)
        {
            dogStatus = DogStatus.Leave;
        }
    }

    private void LateUpdate()
    {
        switch(dogStatus)
        {
            case DogStatus.Follow:
                if (time > actionTime)
                {
                    if (Vector3.Distance(transform.position, playerTransform.position) >= minDistance && Vector3.Distance(transform.position, playerTransform.position) <= minDistance + moveBuffer)
                    {
                        //Debug.Log("1st");
                        if (flag == 0)
                        {
                            FollowWithPlayer();
                        }
                        else
                        {
                            transform.position = Vector3.Lerp(transform.position, playerTransform.position, coefficient);
                            //Debug.Log("3rd");
                            if (Vector3.Distance(transform.position, playerTransform.position) < 0.01f)
                            {
                                flag = 0;
                            }
                        }
                        dogAnimator.SetBool("isWalk", true);
                    }
                    else if (Vector3.Distance(transform.position, playerTransform.position) > minDistance + moveBuffer)
                    {
                        //Debug.Log("2nd");
                        FollowWithPlayer();
                        flag = 1;
                        dogAnimator.SetBool("isWalk", true);
                    }
                }
                break;
            case DogStatus.Leave:
                Leave();
                break;

        }

        if (Mathf.Abs(Vector3.Distance(transform.position, playerTransform.position) - minDistance) < 0.05f)
        {
            dogAnimator.SetBool("isWalk", false);
            time = 0;
        }
        if(dogStatus == DogStatus.Follow)
        {
            if (playerTransform.position.x > transform.position.x)
            {
                dogAnimator.SetBool("isLeft", false);
            }
            else
            {
                dogAnimator.SetBool("isLeft", true);
            }
        }
    }

    private void FollowWithPlayer()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime);
    }

    private void Leave()
    {
        transform.Translate(Vector3.right * Time.deltaTime * leaveSpeed);
    }
    private void OnBecameInvisible()
    {
        if(transform.position.x < 0)
        {
            return;
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
