using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        //Debug.Log("Awake");
    }
    private void OnEnable()
    {
        //Debug.Log("Enable");
    }
    private void Update()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;
        transform.right = temp - transform.position;
    }
    private void OnDisable()
    {
       //Debug.Log("Disable");
    }
}
