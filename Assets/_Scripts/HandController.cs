using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandController : MonoBehaviour {

    public static HandController Instance { get; private set; }

    public FoodSprite currentSelectFood;

    void Awake()
    {
        Instance = this;

  
#if UNITY_EDITOR
        Application.runInBackground = true;
#endif
    // Application.targetFrameRate = 60;
    }




    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentSelectFood)
        {
            //  currentSelectFood.transform.DOMove(Input.mousePosition, 0f);

            currentSelectFood.transform.position = GameManager.Instance.GetMousePosition();
        }
	}

    public bool IsIdle
    {
        get
        {

            if (!currentSelectFood)
                return true;
            else
                return false;
        }
    }


    //public bool PickFood(Food food)
    //{
    //    if (IsIdle)
    //    {
    //        currentSelectFood = food;
   

    //        print("选中了食物" + food.name);
    //        return true;
    //    }
    //    return false;
    //}

    void PutFood(FoodSprite food, IFoodHolder holder)
    {

    }




    public Tween PutFood(IFoodHolder holder, out FoodSprite food,bool isEatRaw)
    {
        //如果手上没有肉，那就返回空
        if (IsIdle)
        {
            food = null;
            return null;
        }

     
        if (!isEatRaw && (currentSelectFood.foodData.maxHp - currentSelectFood.foodData.hp < Mathf.Epsilon))
        {
            print("生肉不能吃");
            food = null;
            return null;
        }
        else
        {

            if (currentSelectFood && currentSelectFood.CanMove())
            {

                // currentSelectFood.MoveToPosition(holder.GetFoodPosition());

                Tween tempTween = currentSelectFood.transform.DOMove(holder.GetFoodPositionTransform().position, 4f);

                FoodSprite temp = currentSelectFood;
                currentSelectFood = null;
                print("放下了食物" + temp.foodName);
                //  return temp;
                food = temp;
                return tempTween;
            }
            else
                food = null;
                return null;
        }

        }



    public void CollectMoney  (Money money)
    {
        //处理一些相关信息，例如是否得双倍钱之类的
        money.DestroyMoney();
    }

    public void DragEnd()
    {
        print("松手了");

    }
}
