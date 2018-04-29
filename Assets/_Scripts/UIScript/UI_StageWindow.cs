using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StageWindow : UI_WindowScriptBase{
    public int stageIndex;

    public Button btn_Close;
    public Button btn_StartGame;
    public Text txt_StageName;

    private void Awake()
    {
        btn_Close.onClick.AddListener(() => { CloseWindow(); });
        btn_StartGame.onClick.AddListener(() => { StartGame(); });


    //    txt_StageName.text = "" + stageIndex;
    }

    public void SetStageName(string value)
    {

        txt_StageName.text = R.UI_String.第 +value+ R.UI_String.关;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        //  MySceneManager.Instance.ChangeScene(GameConfig.SceneName.mainTest);

        CoverUIManager.Instance.StartStageGame(1);


    }
}
