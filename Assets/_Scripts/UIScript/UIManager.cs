using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {

    public static UIManager Instance { get; private set; }

    public RectTransform winDialogPrefab;
    public Transform dialogRoot;



    public LevelRatingScript levelRating;
    public HeartScript heartGroup;

    public Text customerCountText;//顾客数量
    public Text scoreText;


    private bool isDone = false;

    private void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start() {
        //float aa = 1;
        //float bb = 100;

        //DOTween.To(() =>  aa, x =>aa=x, 3,2);

        scoreText.text = "0";

    }

    public void InitData()
    {
        SetCustomerCount(GameManager.Instance.stageData.customerCount);


    }


    // Update is called once per frame
    void Update() {


    }

    public void SetCustomerCount(int value)
    {
        customerCountText.text = "" + value;
    }

    //设置关卡星级状态，UI管理者只需要知道比率和星星数量
    public void SetLevelRating(float rate, int starCount)
    {
        levelRating.SetHpBar(rate).OnComplete(() => {
            levelRating.SetStarCount(starCount);
        });


    }

    public void SetHeartCount(int value)
    {
        heartGroup.SetHp(value);
    }

    private Tween tween = null;
    private string tempString;
    private int tempInt;

    public void SetScore(int value)
    {
        tempString = scoreText.text;
        tempInt = int.Parse(tempString);

        if (tempInt != value)
        {
            isDone = false;
            if (tween != null)
                tween.Complete(true);

            tween = DOTween.To(() => tempInt, x => tempInt = x, value, 1).OnComplete(Done);
            StartCoroutine(RefreshScoreText());
        }


    }

    public void Done()
    {
        isDone = true;
    }

    IEnumerator RefreshScoreText()
    {
        while (!isDone)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            scoreText.text = "" + tempInt;
        }
    }

    public void ShowWinDialog()
    {
        ShowDialogBg();//显示遮黑挡板
        RectTransform rect = Instantiate(winDialogPrefab, dialogRoot);        
        EaseOutBack(rect);
    }

    private void ShowDialogBg()
    {
        dialogRoot.gameObject.SetActive(true);
    }

    public void ShowLoseDialog()
    {
        print("弹出失败窗口");
    }


    //静态方法区域


     //从极小弹到大
    public static void EaseOutBack(RectTransform rect)
    {
        rect.DOScale(0.2f, 0);
        rect.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }
}
