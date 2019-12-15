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
    private GameObject dogDialog;
    private GameObject momDialog;

    /*Text*/
    private GameObject text;
    private GameObject dogText;
    private GameObject momText;
    private Text _text;
    private Text _dogText;
    private Text _momText;

    /*狗*/
    private GameObject dog;
    private GameObject player;

    /*画布*/
    public GameObject canvas;
    
    private int i;
    private int j;

    private float localTime;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox = GameObject.FindWithTag("Dialog");
        dogDialog = GameObject.FindWithTag("Dialog2");
        momDialog = GameObject.FindWithTag("Dialog3");

        text = GameObject.FindWithTag("GirlText");
        _text = text.GetComponent<Text>();
        dogText = GameObject.FindWithTag("DogText");
        _dogText = dogText.GetComponent<Text>();
        momText = GameObject.FindWithTag("MomText");
        _momText = momText.GetComponent<Text>();

        dog = GameObject.FindWithTag("Dog");
        player = GameObject.FindWithTag("Player");
        
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(WorldToUIPos(dialogBox.transform.position) + "dialogBox.transform.position");
        //Debug.Log(text.transform.position - canvas.transform.position + "text.transform.position");
        if(dog.transform.position.x > -23.5f)
            localTime += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space) && i == 1)
        {
            dialogBox.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1);
            text.GetComponent<Text>().DOColor(new Color(1, 1, 1, 1), 1);
            _text.DOText("    有人吗！", 1).OnComplete(Text1);
            i++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && i == 0)
        {
            //_dogText.DOText("   wang", 1);
            dialogBox.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1);
            text.GetComponent<Text>().DOColor(new Color(1, 1, 1, 1), 1);
            _text.DOText("    喂！", 1).OnComplete(Text1);
            i++;
        }

        //即狗进入视野范围
        if ((int)localTime == 1)
        {
            _dogText.DOText("   汪...汪...", 1).OnComplete(Text2);
            dogDialog.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1);
            dogText.GetComponent<Text>().DOColor(new Color(1, 1, 1, 1), 1);
            //_text.DOText("    喂！", 1).OnComplete(Text1);
        }

        if ((int) player.transform.position.x == 21 && j == 0)
        {
            _momText.DOText("    回来就好", 1).OnComplete(Text3);
            momDialog.transform.DOScale(new Vector3(0.3f, 0.3f, 1), 1);
            momText.GetComponent<Text>().DOColor(new Color(1, 1, 1, 1), 1);
            j++;
        }

        text.transform.position = canvas.transform.position + WorldToUIPos(dialogBox.transform.position);

        dogText.transform.position = canvas.transform.position + WorldToUIPos(dogDialog.transform.position);

        momText.transform.position = canvas.transform.position + WorldToUIPos(momDialog.transform.position) + new Vector3(0,10.0f,0);
    }

    private void Text1()
    {
        
        dialogBox.transform.DOScale(new Vector3(0, 0, 1), 1).SetDelay(2);
        text.GetComponent<Text>().DOColor(new Color(1, 1, 1, 0), 1).SetDelay(1);
    }

    private void Text2()
    {
        dogDialog.transform.DOScale(new Vector3(0, 0, 1), 1).SetDelay(2);
        dogText.GetComponent<Text>().DOColor(new Color(1, 1, 1, 0), 1).SetDelay(1);
    }

    private void Text3()
    {
        momDialog.transform.DOScale(new Vector3(0, 0, 1), 1).SetDelay(2);
        momText.GetComponent<Text>().DOColor(new Color(1, 1, 1, 0), 1).SetDelay(1);
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
