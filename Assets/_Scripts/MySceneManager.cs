using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour {

    public Text barText;

    public UI_HpBarScript loadingBar;


    private AsyncOperation loadingOP;



    void Start () {

            BeginLoading();

    }


    //public static void  ChangeScene(string sceneName)
    //{
    //    MySceneManager.Instance.nextSceneName = sceneName;
    //    SceneManager.LoadSceneAsync(R.SceneName.lodingScene, LoadSceneMode.Single).completed+= LoadingCompleted();
    //}

    //private static Action<AsyncOperation> LoadingCompleted()
    //{
    //    isLoadingCompleted = true;
    //    print("完成代理");
    //    return null;
    //}


    public void BeginLoading()
    {

        StartCoroutine(LoadScene());

    }


    private IEnumerator LoadScene()
    {
        
        yield return new WaitForSeconds(0.5f) ;//测试用

        if (string.IsNullOrEmpty(CSceneManager.Instance.nextSceneName) == false)
        {

            CSceneManager.Instance.lastSceneName = CSceneManager.Instance.nextSceneName;
            loadingOP = CSceneManager.Instance.ChangeScene(CSceneManager.Instance.nextSceneName);
            loadingOP.allowSceneActivation = false;

            while (loadingOP.progress < 0.9f)
            {

                barText.text = (loadingOP.progress*100).ToString("f1") + "%";
                print("" + (loadingOP.progress * 100).ToString("f1") + "%");

                loadingBar.SetHpRate((loadingOP.progress),false);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
            barText.text = 100 + "%";

            Tween tween=
            loadingBar.SetHpRate(1f, false);

            while (!loadingBar.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            loadingOP.allowSceneActivation = true;
        }

    }


}
