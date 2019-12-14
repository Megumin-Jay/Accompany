using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;

    /*左右边界*/
    private float leftBorder;
    private float rightBorder;

    /*摄像机跟随人物的阈值(人物当前位置)*/
    private float leftThreshold;
    private float rightThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        leftBorder = -19;
        rightBorder = 19;

        leftThreshold = -18.5f;
        rightThreshold = 18.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= leftThreshold || player.transform.position.x <= rightThreshold)
            this.transform.position = Vector3.Lerp( this.transform.position,new Vector3(player.transform.position.x, this.transform.position.y,
                    this.transform.position.z), Time.deltaTime);
        
        //边界限制
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, leftBorder, rightBorder), 0
        ,this.transform.position.z);
    }
}
