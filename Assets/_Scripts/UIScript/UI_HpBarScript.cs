using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class UI_HpBarScript : MonoBehaviour {

    public RectTransform bar;
    public Transform bg;
    public Text text; 


    public float maxHp;
    public float realHp;
    private float curHp;

    private Vector2 originalSize;

    public  float hpMoveSpeed=0.5f;

    public bool isDone;

    private void Awake()
    {
        originalSize = bar.sizeDelta;
   }
    // Use this for initialization
    void Start () {
        SetHpRate(0,true);

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

    public Tween SetHpRate(float rate, bool isInstant)
    {
        if (isInstant)
        {
            isDone = false;
            Tween tween=
             bar.DOSizeDelta(new Vector2(originalSize.x * rate, originalSize.y), 0f);
            tween.OnComplete(done);

            return tween;

        }
        else
        {
            isDone = false;
            Tween tween =
            bar.DOSizeDelta(new Vector2(originalSize.x * rate, originalSize.y), hpMoveSpeed);
            tween.OnComplete(done);
            return tween;
        }

    }


    void done()
    {
        isDone = true;
        curHp = realHp;
        print("完成");
    }
}

