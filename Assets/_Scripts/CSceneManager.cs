
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager
{
    private static CSceneManager instance;
    public static CSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CSceneManager();
                Debug.Log("创建了单例CSceneManager");             
            }
            return instance;
        }
    }


    public string nextSceneName = "";
    public string lastSceneName = "";
    public bool ableGO;

    public static bool isLoadingCompleted = false;

    public AsyncOperation asyncO;




    public NavigationData navigationData;

    private CSceneManager()
    {
        navigationData = new NavigationData(true);
    }




    //直接跳转到指定页面
    public AsyncOperation ChangeScene(string sceneName)
    {
        isLoadingCompleted = false;
        asyncO = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncO.completed += LoadingCompleted();

        return asyncO;
    }

    //先跳转到Loading界面再进入指定页面
    public void LoadingScene(string sceneName)
    {
        nextSceneName = sceneName;
        ChangeScene(R.SceneName.lodingScene);
    }


    private  Action<AsyncOperation> LoadingCompleted()
    {
        isLoadingCompleted = true;
        Debug.Log("完成代理");
        return null;
    }

    

}

public struct NavigationData
{
    public bool showStagePage;
    public int currentStageIndex;

    public NavigationData(bool isNewGame)
    {
        showStagePage = false;
        currentStageIndex = 0;
    }
}

