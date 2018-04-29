using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UI_HpBarScript : MonoBehaviour {

    public RectTransform bar;
    public Transform bg;
     
    public float maxHp;
    public float realHp;
    private float curHp;

    private Vector2 originalSize;

    private float hpMoveSpeed=1f;

    private void Awake()
    {
        originalSize = bar.sizeDelta;
   }
    // Use this for initialization
    void Start () {
        SetHpRate(0);

    }
	
	// Update is called once per frame
	void Update () {

        //if (realHp != curHp)
        //{
        //    // bar.DOScaleX(0f, 3);

        //    bar.DOSizeDelta(new Vector2(originalSize.x*realHp/maxHp, originalSize.y), hpMoveSpeed).OnComplete(done);
        //}

	}

    public void SetRealHp(float hp )
    {
        realHp = hp;
        realHp = Mathf.Min(0, realHp);
        realHp = Mathf.Max(realHp, maxHp);

        bar.DOSizeDelta(new Vector2(originalSize.x * realHp / maxHp, originalSize.y), hpMoveSpeed).OnComplete(done);
    }

    public Tween SetHpRate(float rate)
    {

     return  bar.DOSizeDelta(new Vector2(originalSize.x * rate, originalSize.y), hpMoveSpeed);
    }


    void done()
    {
        curHp = realHp;
        print("完成");
    }
}

