using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : MonoBehaviour
{
    private Vector3 dogPosition;
    private Vector3 playerPosition;

    [Header("狗和人的最小距离")]
    [Range(0, 5)]
    [SerializeField]
    private float minDistance;//
    private void Awake()
    {
        dogPosition = this.transform.position;
        //TODO
        //找到player的Transform组件
        //playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }
    private void LateUpdate()
    {
        if(Vector3.Magnitude(dogPosition - 
            //注意要改成playerPosition
            Input.mousePosition) > minDistance)
        {

        }
    }

    private void FollowWithPlayer()
    {

    }
}
