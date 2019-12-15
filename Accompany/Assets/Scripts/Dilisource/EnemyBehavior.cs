using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Transform playerTransform;
    public float speed;
    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if(playerTransform == null)
        {
            return;
        }
    }

    private void Update()
    {
        ApproachPlayer();
    }

    private void ApproachPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "BarkWave(Clone)")
        {
            StartCoroutine(SpeedRecover());
        }
    }

    IEnumerator SpeedRecover()
    {
        yield return new WaitForSeconds(1.5f);
        speed = 0.5f;
    }
    private void OnDisable()
    {
        speed = 0.5f;
    }
}
