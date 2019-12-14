using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollowPhysically : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float maxSpeed;

    private float nowSpeed;
    private float accelerateTime;
    [Header("狗和人的最小距离")]
    [Range(0, 5)]
    [SerializeField]
    private float minDistance;

    private Vector3 targetPos;
    private void Awake()
    {
        accelerateTime = maxSpeed / acceleration;
    }
    private void FixedUpdate()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        if (acceleration * accelerateTime * accelerateTime < Vector3.Distance(transform.position,targetPos))
        {
        }
    }

}
