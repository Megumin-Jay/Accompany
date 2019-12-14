using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerEnvir : MonoBehaviour
{
    /*初始光强*/
    private float localIntensity;
    
    // Start is called before the first frame update
    void Start()
    {
        localIntensity = 0.96f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "SwitchLight")
        {
            float currentIntensity = col.gameObject.GetComponent<Light2D>().intensity;
            col.gameObject.GetComponent<Light2D>().intensity = localIntensity - currentIntensity;
        }
    }
}
