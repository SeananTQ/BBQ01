using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CoverUIManager : MonoBehaviour {

    public static CoverUIManager Instance  {get;private set;}
    //引用区
    public RectTransform win_Stage;
    public Transform dialogRoot;

    public Button button_StageStartGame;
    public Transform stagePageTf;
    public Button button_StagePage_Back;

    //以上

    private void Awake()
    {
        Instance = this;
    }

    void Start () {

        //开始关卡模式按钮
        button_StageStartGame.onClick.AddListener(() => {
      
            ShowStagePage(true);
        });

        button_StagePage_Back.onClick.AddListener(() => {

            ShowStagePage(false);
        });


        CheackPageData();


    }


    private void CheackPageData()
    {
        if (CSceneManager.Instance.navigationData.showStagePage)
        {
            ShowStagePage(true);

        }

    }

	void Update () {
		
	}

    public void ShowStageWindow(int index)
    {
        RectTransform rect =  Instantiate(win_Stage, dialogRoot);
        // RectTransform rect = temp.GetComponent<RectTransform>();

        UI_StageWindow temp = rect.GetComponent<UI_StageWindow>();

        if (temp &&rect)
        {

            UIManager.EaseOutBack(rect);

            //rect.DOScale(0.2f, 0);
            //rect.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

            // rect.DOBlendableScaleBy(new Vector3(0.2f,0.2f,1), 0).SetEase(Ease.OutBack);

            temp.SetStageName("" + index);

        }
        
    }

    public void StartStageGame(int stageIndex)
    {
        //TODO 这里应该存储一些关卡数据
        CSceneManager.Instance.LoadingScene(R.SceneName.mainGame);
    }

    public void ShowStagePage(bool value)
    {
        if (value)
        {

        }
        else
        {

        }
        stagePageTf.gameObject.SetActive(value);

    }

}
