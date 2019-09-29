using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_WinStageScript : MonoBehaviour {

    public Button button_restart;
    public Button button_next;


    // Use this for initialization
    void Start () {
        button_restart.onClick.AddListener(Go);

    }

    //跳转到指定界面
    private void Go()
    {
        NavigationData temp = new NavigationData();
        temp.showStagePage = true;
        temp.currentStageIndex = 3;
        CSceneManager.Instance.navigationData = temp;
        CSceneManager.Instance.LoadingScene(R.SceneName.navigation);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
