using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dish : MonoBehaviour, IFoodHolder
{

    public Transform foodPrefab;

    public Transform foodPosition;

    private FoodSprite currentFood;

    public ObjectNameID prefabObjectID;

    void Start()
    {


        //  currentFood= CreateRaw().GetComponent<Food>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (GameManager.Instance.isGamePlaying)
        {
            if (currentFood == null)
            {
                currentFood = CreateRaw();
                currentFood.CurrentfoodHolder = this;
                currentFood.gameObject.SetActive(true);
            }
        }
    }

    public bool CanCreateRaw()
    {
        //需要判断钱够不够买肉之类的
        //判断手是否空闲
        if (HandController.Instance.IsIdle)
        {

            return true;
        }

        return false;
    }

    private FoodSprite CreateRaw()
    {
        Transform tempTf;
        tempTf = ObjectPoolScript.Instance.GetObject(foodPrefab, prefabObjectID);
        tempTf.SetParent(foodPosition,false);
        //tempTf.position = foodPosition.position;
        FoodSprite tempFood = tempTf.GetComponent<FoodSprite>();

        tempFood.CurrentfoodHolder = this;

        return tempFood;

    }


    public void FoodAway()
    {
        currentFood = null;
    }

    public Transform GetFoodPositionTransform()
    {
        return foodPosition;
    }

    public bool TakeFood(FoodSprite food)
    {
      //  print("==" + food.foodData.foodType + "==" + foodPrefab.GetComponent<FoodSprite>().foodData.foodType);


        if (food.foodData.foodType != currentFood.foodData.foodType || food.IsIntact == false)
        {
     
            return false;
        }
        else
        {

            if (currentFood != null)
            {
                ObjectPoolScript.Instance.RecycleObject(currentFood.transform, prefabObjectID);
            }

            currentFood = food;

            print("currentFood " + currentFood.foodData.foodName);
            Tween tween=
            currentFood.transform.DOMove(foodPosition.position, GameConfig.FOOD_MOVE_TIME);

            tween.OnComplete(currentFood.FinshMoving);

            return true;
        }

    }
}
