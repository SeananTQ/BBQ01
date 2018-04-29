using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour {
    public static PlayerDataManager Instance { get; private set; }

    public PlayerData playerData;

    private void Awake()
    {
        Instance = this;

        playerData = new PlayerData();
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
