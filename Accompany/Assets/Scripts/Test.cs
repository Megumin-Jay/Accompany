using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
    }
    private void OnEnable()
    {
        Debug.Log("Enable");
    }
    private void Update()
    {
    }
    private void OnDisable()
    {
        Debug.Log("Disable");
    }
}
