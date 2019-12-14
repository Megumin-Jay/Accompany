using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICon : MonoBehaviour
{    
    /*上拉菜单*/
    private GameObject pullUpMenu;
    /*结束菜单*/
    private GameObject gameOverMenu;
    /*目标位置*/
    private Vector3 pullTargetPos;
    private Vector3 gameOverTargetPos;
    /*是否允许上拉*/
    private bool canPull;
    /*游戏是否结束*/
    private bool isOver;
    
    /*声音组件*/
    private AudioSource _audioSource;
    /*点击音效*/
    public AudioClip startClick;


    //单例
    private static UICon instance;
    public static UICon Instance
    {
        get
        {
            return instance;
        }
    }

    public bool CanPull
    {
        get => canPull;
        set => canPull = value;
    }

    protected void Awake()
    {
        instance = this as UICon;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        pullUpMenu = GameObject.FindWithTag("PullUpMenu");
        gameOverMenu = GameObject.FindWithTag("GameOverMenu");
        _audioSource = GameObject.FindWithTag("MusicController").GetComponent<AudioSource>();
        
        pullTargetPos = new Vector3(960.0f, 1630.0f, 0.0f);
        gameOverTargetPos = new Vector3(960.0f,540.0f,0.0f);
        
        canPull = false;
        isOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canPull)
            pullUpMenu.transform.position = Vector3.Lerp(pullUpMenu.transform.position, pullTargetPos, Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
            isOver = true;
        if (isOver)
        {
            gameOverMenu.transform.position = Vector3.Lerp(gameOverMenu.transform.position, gameOverTargetPos, Time.deltaTime);
        }
    }

    /// <summary>
    /// 上拉菜单
    /// </summary>
    public void PullUp()
    {
        canPull = true;
        
        //点击音效
        _audioSource.PlayOneShot(startClick);
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOverUI()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
