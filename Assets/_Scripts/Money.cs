using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour , IPoolElement
{


    public int count;


    public float stayTime=3f;

    public bool isMoving=false;
    public bool isUsed = false;

	void Start () {


        
   //     InitData();
    }

    public void InitData()
    {
        //在地上停留一小段那时间
        //  StayAndDestroy();
        //print("初始化数据");

        count = 100;
        transform.DOScale(new Vector3(1f, 1f, 1),0);
    }


    // Update is called once per frame
    void Update () {
		
	}

    private void OnMouseUp()
    {
        GameManager.Instance.SetLevelRating(count);
        isUsed = true;

        Tween tween=
        transform.DOMove(GameManager.Instance.tokenBarRoot.position, 0.3f);
        isMoving = true;
        tween.OnComplete(()=> {

            isMoving = false;
            GameManager.Instance.SetScore(count);//增加金币数量
            transform.DOScale(new Vector3(0.1f, 0.1f, 1), 0.3f).SetEase(Ease.OutBack).OnComplete(()=> {

                ObjectPoolScript.Instance.RecycleObject(this.transform, ObjectNameID.MONEY_1);

            });


        });
    }




    public void StayAndDestroy()
    {
        StartCoroutine(WaitToDestroy(stayTime));
        
    }


    public void DestroyMoney()
    {        
        StartCoroutine(WaitToDestroy(0.3f));
    }


    IEnumerator WaitToDestroy(float time)
    {
      //  print("3秒后消失");
        yield return new WaitForSeconds(time);
      //  print("执行消失");

        ObjectPoolScript.Instance.RecycleObject(this.transform, ObjectNameID.MONEY_1);

    }



    public Transform GetTransform()
    {
        return transform;
    }
}
