using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    /*对话框*/
    private GameObject dialogBox;

    private GameObject text;
    private Text _text;

    /*画布*/
    public GameObject canvas;
    private Sequence _sequence;
    
    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox = GameObject.FindWithTag("Dialog");
        text = GameObject.FindWithTag("GirlText");
        _text = text.GetComponent<Text>();
        _sequence = DOTween.Sequence();
        
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(WorldToUIPos(dialogBox.transform.position) + "dialogBox.transform.position");
        Debug.Log(text.transform.position - canvas.transform.position + "text.transform.position");
        
        if (Input.GetKeyDown(KeyCode.Space) && i == 1)
        {
            dialogBox.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1);
            text.GetComponent<Text>().DOColor(new Color(1, 1, 1, 1), 1);
            _text.DOText("    有人吗！", 1).OnComplete(Text1);
            i++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && i == 0)
        {
            dialogBox.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1);
            _text.DOText("    喂！", 1).OnComplete(Text1);
            i++;
        }

        text.transform.position = canvas.transform.position + WorldToUIPos(dialogBox.transform.position);
    }

    private void Text1()
    {
        
        dialogBox.transform.DOScale(new Vector3(0, 0, 1), 1).SetDelay(1);
        text.GetComponent<Text>().DOColor(new Color(1, 1, 1, 0), 1).SetDelay(1);
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
