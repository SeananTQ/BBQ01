using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageIconScript : MonoBehaviour {

    private Button thisButton;
    private Text text;
    private  string stageIndex;
    private void Awake()
    {
        thisButton = GetComponent<Button>();
        text=transform.GetComponentInChildren<Text>();
        stageIndex = transform.name;
        stageIndex = stageIndex.Substring(stageIndex.IndexOf("_")+1);

        text.text = stageIndex;

        if (thisButton == null)
        {
            Debug.LogError("脚本没有挂在按钮上");
        }
        else
        {
            thisButton.onClick.AddListener(ShowWindow);

        }
    }

    private void ShowWindow()
    {

        CoverUIManager.Instance.ShowStageWindow( int.Parse(stageIndex));

        Debug.Log("选中了这个关卡:"+stageIndex);

    }

}
