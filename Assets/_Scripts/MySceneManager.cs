using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {
    public static MySceneManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string sceneName)
    {

       SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
