using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBark : MonoBehaviour
{
    [SerializeField]
    private float radius;
    private LayerMask layerMask = 1 << 8;

    [SerializeField]
    private float intervalTimeOfBark;
    private float time;

    [SerializeField]
    private GameObject barkWave;
    private void Awake()
    {
        //layerMask = LayerMask.NameToLayer("Enemy");
    }
    private void Update()
    {
        
        time += Time.deltaTime;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (collider != null)
        {
            if(collider.transform.CompareTag("Enemy") && time > intervalTimeOfBark)
            {
                if(collider.GetComponent<EnemyBehavior>().speed == 0.5f)
                {
                    Bark(collider.transform.position);
                    time = 0;
                }
            }
        }
    }

    private void Bark(Vector3 target)
    {
        Vector3 temp = Vector3.Normalize(target - transform.position);
        GameObject go = Instantiate(barkWave);
        go.transform.position = transform.position + temp * 0.5f;//是否需要一个系数
        go.transform.right = temp;
        go.GetComponent<Rigidbody2D>().velocity = temp;//要不要加一个系数

    }

}
