using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trash : MonoBehaviour, IFoodHolder
{

    public Transform foodPosition;
    FoodSprite currentFood;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUp()
    {

        Tween tempTween = HandController.Instance.PutFood(this, out currentFood, false);


        if (currentFood && tempTween != null)
        {
            //   tempTween.OnComplete(currentFood.DestroySelf);
            DestroyFood(currentFood, tempTween);
        }
    }

    public void FoodAway()
    {

    }

    public void DestroyFood(FoodSprite food, Tween tween)
    {
        if (food && tween != null)
        {
     
            tween.OnComplete(food.DestroySelf);
       //     tween.OnComplete(TrashEffect);
        }
    }


    public void TrashEffect()
    {
        //   print("销毁食物同时播放的丢垃圾的声音");

        print("扣钱"+"丢垃圾的声音");
    }



    public Transform GetFoodPositionTransform()
    {
        return foodPosition;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
     
    }

    public bool TakeFood(FoodSprite food)
    {
        //判断食物是否是生的
        if (food.IsIntact)
        {
            return false;

        }

        else
        {
            //food.transform.DOMove(foodPosition.position, GameConfig.FOOD_OFFSET_TIME);


           food.DestroySelf();
        }
            return true;

    }
}
