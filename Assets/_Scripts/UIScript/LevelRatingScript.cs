using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRatingScript : MonoBehaviour {

    public List<UI_StarScript> starList;

    public  UI_HpBarScript hpBar;

    private void Awake()
    {

    }
    void Start () {

    }
	

	void Update () {
		
	}

    public Tween SetHpBar(float rate)
    {
       return  hpBar.SetHpRate(rate,false);
    }

    public void SetStarCount(int count)
    {
        for (int i = 0; i < starList.Count; i++)
        {
            if (i < count)
            {
                starList[i].SetLight(true);
            }
            else
            {
                starList[i].SetLight( false);
            }
        }
    }
}
