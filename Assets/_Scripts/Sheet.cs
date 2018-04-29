using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sheet : MonoBehaviour, IFoodHolder
{

    public string sheetName;

    public Transform foodPosition;

    public Animator animator;

    public FoodSprite currentFood;
    public float attack;

    public bool canAttack=false;

    enum SHEET_STATE
    {
        IDEL,
        COOKING,
        MALFUNCTION,
    }


    void Start() {

        animator = transform.GetComponent<Animator>();
        attack = 2;

    }

    // Update is called once per frame
    void Update() {

        if (!IsIdle && canAttack)
        {  
            if (!currentFood.HeatFood(GetDamage(attack)))
            {
                animator.SetInteger("State", 2);
                CanWoke = false;
                // print("烤炉坏了需要修理");
            }

        }
            
    }

    private bool IsIdle
    {
        get
        {
            if (!currentFood)
            {
                return true;
            }
            else
                return false;
        }

    }

    public void Cooking()
    {
        animator.SetInteger("State", 1);
        canAttack = true;

    }


    public bool CanWoke=true;

    private float  GetDamage(float attack)
    {
        float temp = attack * Time.deltaTime;

        return temp;
    }




    private void OnMouseUp()
    {

    }

    public void FoodAway()
    {
        currentFood = null;
        //还要关掉火特效和音效
        if (CanWoke)
        {
            animator.SetInteger("State", 0);
        }
    }

    public Transform GetFoodPositionTransform()
    {
        return foodPosition;
    }

    public bool TakeFood(FoodSprite food)
    {
        //判断自己是否可以使用

        //判断自己身上是否有食物
        if (IsIdle)
        {
            currentFood = food;
            Tween tween =
            currentFood.transform.DOMove(foodPosition.position, GameConfig.FOOD_OFFSET_TIME);

            tween.OnComplete(Cooking);
            return true;
        }
        else
        {

            return false;
        }


    }


}
