using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogBox : MonoBehaviour
{
    /*对话框*/
    private GameObject dialogBox;

    private GameObject text;

    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox = GameObject.FindWithTag("Dialog");
        text = GameObject.Find("Canvas/Text");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(WorldToUIPos(dialogBox.transform.position) + "dialogBox.transform.position");
        Debug.Log(text.transform.position - canvas.transform.position + "text.transform.position");
        text.transform.position = canvas.transform.position + WorldToUIPos(dialogBox.transform.position);
    }

    /// <summary>
    /// 世界坐标转UI坐标（localPosition）
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Vector3 WorldToUIPos(Vector2 pos)
    {
        //得到画布尺寸
        Vector2 UISize = canvas.GetComponent<RectTransform>().sizeDelta;
        //将世界坐标转为屏幕坐标
        Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
        
        //转换为以屏幕中心为原点的屏幕坐标
        Vector2 screenPos2;
        screenPos2.x = screenPos.x - Screen.width / 2;
        screenPos2.y = screenPos.y - Screen.height / 2;

        Vector3 uiPos;
        uiPos.x = screenPos2.x / Screen.width * UISize.x;
        uiPos.y = screenPos2.y / Screen.height * UISize.y;
        uiPos.z = 0;
        return uiPos;
    }
}
