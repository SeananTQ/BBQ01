using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject mainCanvas;

    public GAME_MODEL gameModel = GAME_MODEL.STAGE;
    public bool isGamePlaying = false;

    public StageData stageData;
    private int currentColdCount;
    public int heartCount;
    private int maxHeartCount = 3;
    public int currentScore;


    public GAME_STATE gameState = GAME_STATE.LOAD;

    public ContactFilter2D contactFilter2D;//过滤规则

    public Dictionary<int, FoodData> foodDataBase;
    public string foodSheetName;

    public Transform tokenBarRoot;

    private void Awake()
    {
        Instance = this;

        tokenBarRoot =  GameObject.Find("TokenBar").transform;

        if (mainCanvas.activeSelf == false)
        {
            mainCanvas.SetActive(true);
        }
    }

    void Start()
    {
        Screen.SetResolution(540, 960, false);
        contactFilter2D = new ContactFilter2D();
#if UNITY_EDITOR
        foodDataBase = FoodData.ReadTable(TableLoader.Instance.GetSheetDataInEditor(foodSheetName));
#else
        foodDataBase = FoodData.ReadTable(TableLoader.Instance.GetSheetData(foodSheetName));
#endif





        //判断是哪种玩法
        //如果是关卡模式
        if (gameModel == GAME_MODEL.STAGE)
        {
            //读取关卡相关数据
            stageData = new StageData();
            //本关有多少名顾客，过关需要多少金币，顾客数据
            // 读取玩家能力属性，每种菜品的等级
            heartCount = maxHeartCount;
            currentScore = 0;

            //读取完毕


            //初始化自己类的数据

            currentColdCount = 0;
        }



        //显示UI
        UIManager.Instance.InitData();
        //顾客来临倒计时

        //开始游戏
        gameState = GAME_STATE.PLAY;
        isGamePlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{

        //    print("鼠标送了");
        //}
    }

    public Vector3 GetMousePosition()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;

        return temp;
    }

    public void SetCustomerCount(int value)
    {
        UIManager.Instance.SetCustomerCount(value);
    }

    //只需要传本次获得金币数量就行
    public void SetLevelRating(int goldCount)
    {
        currentColdCount += goldCount;

        float tempRate =    (float )  currentColdCount / stageData.targetGoldCount;
        int tempCount = 0;

        if (tempRate >= 1f)
        {
            tempCount = 3;
            tempRate = 1;
        }
        else if (tempRate >= 0.7f)
        {
            tempCount = 2;
        }
        else if (tempRate >= 0.4f)
        {
            tempCount = 1;
        }
        UIManager.Instance.SetLevelRating(tempRate, tempCount);
    }

    public void SetHeartCount(int value)
    {
        heartCount += value;
        heartCount = Mathf.Min(heartCount, maxHeartCount);

        if (heartCount == 0)
        {
            //进入等死流程

        }

        UIManager.Instance.SetHeartCount(heartCount);
    }

    public void SetScore(int value)
    {
        currentScore += value;
        currentScore = currentScore < 0 ? 0 : currentScore;

        UIManager.Instance.SetScore(currentScore);
    }
}
