using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodTask : MonoBehaviour {

    public Transform foodPosition1;
    public Transform foodPosition2;
    public Transform foodPosition3;

    public Transform hpBar;

    private Transform currentFood;

    private SpriteRenderer hpBarRenderer;

    private Vector2 sizeOriginal;

    private void Awake()
    {
        hpBarRenderer = hpBar.GetComponent<SpriteRenderer>();
        sizeOriginal = hpBarRenderer.size;
     //   hpBarRenderer.DOColor(new Color32(255,0,0,100), 0.3f).SetLoops(-1, LoopType.Yoyo);

    }

    void Start () {
      

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowTask(int index, Transform foodTF)
    {
        //currentFood = ObjectPoolScript.Instance.GetObject(null, ObjectNameID.FOOD_3).GetComponent<FoodSprite>();

        //currentFood.InitData(foodData);

        currentFood = foodTF;
        Transform tempPos;
        if (index == 1)
        {
            tempPos = foodPosition1;
        }
        else
        {
            tempPos = foodPosition2;
        }

        
        currentFood.SetParent(tempPos,false);
     // currentFood.position = tempPos.position;
      //  currentFood.localScale = tempPos.localScale;


    }

    //根据种类回收食物对象
    public void DestoryTask(Transform foodTf)
    {
        foodTf.GetComponent<FoodSprite>().DestroySelf();

        //FoodData tempFoodDate = foodTf.GetComponent<FoodSprite>().foodData;
        //ObjectNameID tempID = ObjectNameID.OTHER;;
        //switch (tempFoodDate.foodType)
        //{
        //    case GameConfig.FOOD_TYPE.FOOD_1:
        //        tempID = ObjectNameID.FOOD_1;
        //    break;
        //    case GameConfig.FOOD_TYPE.FOOD_2:
        //        tempID = ObjectNameID.FOOD_2;
        //        break;
        //    case GameConfig.FOOD_TYPE.FOOD_3:
        //        tempID = ObjectNameID.FOOD_3;
        //        break;
        //    case GameConfig.FOOD_TYPE.FOOD_4:
        //        tempID = ObjectNameID.FOOD_4;
        //        break;

        //}

        //print("销毁" + tempFoodDate.foodName);
        //    ObjectPoolScript.Instance.RecycleObject(foodTf, tempID);

    }

    public void SetHpBar(float rate)
    {
        hpBarRenderer.size = new Vector2(sizeOriginal.x, sizeOriginal.y * rate);

    }

    /// <summary>
    /// 比较提交的食物是否为该顾客所需的种类
    /// </summary>
    /// <param name="food"></param>
    /// <returns>结果正确则返回食物任务的坐标物体，否则返回空</returns>
    public Transform CompareFood(FoodSprite food)
    {
        if (food.foodData.foodType == currentFood.GetComponent<FoodSprite>().foodData.foodType)
        {
            return foodPosition1;
        }

        return null;
    }

    public void TakeFood(FoodSprite food)
    {


    }
}
