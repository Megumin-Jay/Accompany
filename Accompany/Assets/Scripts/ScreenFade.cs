using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public GameObject panel;

    private GameObject text;
    // Use this for initialization
    void Start()
    {
        text = panel.transform.GetChild(0).gameObject;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Color localImageColor = panel.GetComponent<Image>().color;
            panel.GetComponent<Image>().DOColor(new Color(localImageColor.r, localImageColor.g, localImageColor.b, 1), 1);
            Color localTextColor = text.GetComponent<Text>().color;
            text.GetComponent<Text>().DOColor(new Color(localTextColor.r, localTextColor.g, localTextColor.b, 1), 1);
        }
    }
}