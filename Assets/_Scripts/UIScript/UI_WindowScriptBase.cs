using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UI_WindowScriptBase : MonoBehaviour {



	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CloseWindow()
    {
        Destroy(gameObject);
        print("关闭");
    }


}
