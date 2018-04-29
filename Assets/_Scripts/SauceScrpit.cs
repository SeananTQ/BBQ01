using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SauceScrpit : MonoBehaviour
{

    public Transform rootPosition;
    public SAUCE_TYPE type= SAUCE_TYPE.SAUCE_1;

    public bool IsMoving = false;
    public void BeginMove() { IsMoving = true; }
    public void FinshMoving() { IsMoving = false; }

    //拖拽时鼠标偏移量
    private Vector3 mouseOffset = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (IsMoving)
        {
            print("还在移动中，无法被操作");
            return;
        }
        mouseOffset = transform.position - GameManager.Instance.GetMousePosition();
        transform.DORotate(new Vector3(0, 0, 135), GameConfig.FOOD_MOVE_TIME);
    }

    private void OnMouseDrag()
    {
        if (IsMoving)
        {
            print("还在移动中，无法被操作");
            return;
        }
        transform.position = GameManager.Instance.GetMousePosition() + mouseOffset;
    }

    private void OnMouseUp()
    {


        Collider2D[] re = new Collider2D[3];
        transform.GetComponent<Collider2D>().OverlapCollider(GameManager.Instance.contactFilter2D, re);

        for (int i = 0; i < re.Length; i++)
        {


            if (re[i])
            {
              
                FoodSprite tempFood = re[i].transform.GetComponent<FoodSprite>();

                if (tempFood == null)
                {
                    //如果碰到的目标不是食物就返回
                    GoBack();
                    print("目标不在正确");
                }
                else
                {

                    tempFood.AddSauce(this.type);
                    GoBack();
                    print("加酱:"+ tempFood.foodData.foodName);
                    break;
                    //if (tempFood.TakeFood(this))
                    //{
                    //    //如果目标物体成功的拿了食物，那么就更新数据
                    //    //  CurrentfoodHolder.FoodAway();
                    //    CurrentfoodHolder = tempFood;

                    //}
                    //else
                    //{
                    //    //如果碰到的目标不能拿食物，那么就把食物返回原处。
                    //    // transform.DOMove(CurrentfoodHolder.GetFoodPositionTransform().position, GameConfig.FOOD_MOVE_TIME);
                    //    CurrentfoodHolder.TakeFood(this);

                    //}
                }

            }
        }

    }

    private void GoBack()
    {
        transform.DORotate(new Vector3(0, 0, 0), GameConfig.FOOD_OFFSET_TIME);
        transform.DOMove(rootPosition.position, GameConfig.FOOD_OFFSET_TIME);

    }



}
