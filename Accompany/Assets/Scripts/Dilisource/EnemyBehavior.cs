using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Vector3 playerPosition;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            return;
        }
        playerPosition = player.GetComponent<Transform>().position;
    }

}
