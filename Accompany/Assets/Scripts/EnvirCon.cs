﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using DG.Tweening;

public class EnvirCon : MonoBehaviour
{
    private GameObject[] _light2Ds;
    /*计时器*/
    private float localTime;
    /*闪烁间隔*/
    private float intervalTime;
    /*闪烁时长*/
    private float shiningTime;

    // Start is called before the first frame update
    void Start()
    {
        _light2Ds = GameObject.FindGameObjectsWithTag("ShiningLight");
        intervalTime = 3;
        shiningTime = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_light2Ds.Length);
        localTime += Time.deltaTime;
        if (localTime > intervalTime)
        {
            for (int i = 0; i < _light2Ds.Length; i++)
            {

                _light2Ds[i].GetComponent<Light2D>().intensity = 1.44f;
            }
        }
        if (localTime > intervalTime + shiningTime)
        {
            for (int i = 0; i < _light2Ds.Length; i++)
            {

                _light2Ds[i].GetComponent<Light2D>().intensity = 0;
            }
        }
        if (localTime > intervalTime + shiningTime * 2)
        {
            for (int i = 0; i < _light2Ds.Length; i++)
            {

                _light2Ds[i].GetComponent<Light2D>().intensity = 1.44f;
            }
        }
        if (localTime > intervalTime + shiningTime * 3)
        {
            localTime = 0;
            for (int i = 0; i < _light2Ds.Length; i++)
            {
                _light2Ds[i].GetComponent<Light2D>().intensity = 0;
            }
        }
    }
}

