using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Customer : MonoBehaviour, IFoodHolder, IPoolElement
{


    public Transform foodPosition;
    public Transform taskTransform;

    private FoodSprite currentFood;
    public int index;
    public int actorWeight = 100;

    public float maxPatience = 1000;
    public float curPatience;

    public List<Transform> actorList;




    public CUST_STATE state;



    private FoodTask foodTask;
    private bool onOrderFood = false;

    private List<Transform> orderFoodTfList;





    void Start()
    {
        foodTask = taskTransform.GetComponent<FoodTask>();


        InitData();
        // OrderFood();
    }


    public void InitData()
    {
        orderFoodTfList = new List<Transform>();
        curPatience = maxPatience;

        taskTransform.gameObject.SetActive(false);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        state = CUST_STATE.IDLE;

        foreach (Transform tf in actorList)
        {
            tf.gameObject.SetActive(false);
        }
        actorList[Random.Range(0, actorList.Count)].gameObject.SetActive(true);


    }


    void Update()
    {

        switch (state)
        {
            case CUST_STATE.ORDER:
                //如果正在点餐中
                LosePatience();
                break;


            case CUST_STATE.GOHOME:

                GoHome();
                break;
        }




    }



    public void EatFood()
    {

        //通过上一部已经确定currentFood就是正确的是食物
        //判断一下酱料
        //判断一下火候



        Transform tempTf = null;
        //在点餐清单中查找那个符合条件的食物，
        foreach (Transform temp in orderFoodTfList)
        {
            print("开始");
            if (currentFood.foodData.foodType == temp.GetComponent<FoodSprite>().foodData.foodType)
            {
                tempTf = temp;
                break;
            }
        }
        //并从列表中移除
        orderFoodTfList.Remove(tempTf);
        //销毁TASK上的食物
        foodTask.DestoryTask(tempTf);

        print("吃食物" + currentFood.foodName);
        //销毁玩家提交的食物
        currentFood.DestroySelf();
        currentFood = null;

        //判断一下是否全部食物都提交了
        if (orderFoodTfList.Count == 0)
        {
            //如果都提交了就给钱
            CustomerManager.Instance.Payment(this.index);

            //收回任务板
            taskTransform.DOScale(0, 0.2f).OnComplete(() =>
            {
                //然后角色走开
                state = CUST_STATE.GOHOME;
            });
        }
    }

    public void DestroySelf()
    {
        //  ObjectPoolScript.Instance.RecycleObject(transform, ObjectNameID.CUSTOMER_1);
        Destroy(transform.gameObject);

    }



    public Transform GetFoodPositionTransform()
    {
        return foodPosition;
    }

    /// <summary>
    /// 点餐
    /// </summary>
    /// <returns></returns>
    public void OrderFood()
    {
        //根据关卡难度计算顾客随机点餐数量的范围

        //随机出来应该点多少份食物
        int orderCount = 1;
        int index = 1;
        //按照顺序生成食物
        for (int i = 0; i < orderCount; i++)
        {
            if (orderCount == 1)
            {
                index = 1;
            }
            else
            {
                index = i + 2;
            }

            Transform tempFood = RandFoodTask();
            orderFoodTfList.Add(tempFood);
            foodTask.ShowTask(index, tempFood);
        }


        //准备弹出任务面板之前，先将任务面板缩小
        taskTransform.gameObject.SetActive(true);
        taskTransform.localScale = new Vector3(0.01f, 0.01f, 1);
        taskTransform.DOScale(1, 1f).SetEase(Ease.InOutElastic);


        transform.position = new Vector3(transform.position.x, transform.position.y, 0);//还原z轴
        state = CUST_STATE.ORDER;
    }

    public Transform RandFoodTask()
    {
        Transform temp = null;
        int tempRand = Random.Range(0, 4) + 1;//1-4



        switch (tempRand)
        {
            case 1:
                temp = ObjectPoolScript.Instance.GetObject(null, ObjectNameID.FOOD_1);
                break;
            case 2:
                temp = ObjectPoolScript.Instance.GetObject(null, ObjectNameID.FOOD_2);
                break;
            case 3:
                temp = ObjectPoolScript.Instance.GetObject(null, ObjectNameID.FOOD_3);
                break;
            case 4:
                temp = ObjectPoolScript.Instance.GetObject(null, ObjectNameID.FOOD_4);
                break;

        }




        return temp;
    }


    public Transform GetTransform()
    {
        return transform;
    }

    public bool TakeFood(FoodSprite food)
    {
        if (food.IsIntact)//不吃生的食物
        {
            return false;

        }
        else
        {
            //不同玩法还得判断一下是否能够接收类型不对的食物

            foreach (Transform tempTf in orderFoodTfList)
            {
                if (food.foodData.foodType == tempTf.GetComponent<FoodSprite>().foodData.foodType)
                {
                    Tween tween =
                    food.transform.DOMove(tempTf.position, GameConfig.FOOD_OFFSET_TIME);
                    tween.OnComplete(EatFood);
                    currentFood = food;
                    return true;
                }
            }
            return false;
        }
    }

    public void LosePatience()
    {
        this.curPatience -= GameConfig.PATIENCE_ATTACK * Time.deltaTime;

        if (curPatience <= 0)
        {

            foodTask.SetHpBar(0);
            OverTime();
        }
        else
        {
            foodTask.SetHpBar(curPatience / maxPatience);


        }
    }

    public void OverTime()
    {
        state = CUST_STATE.ANGRY;

        //收回任务板
        taskTransform.DOScale(0, 0.2f).OnComplete(() =>
        {
            //对玩家信誉展开攻击
            AttackPlayer();
            state = CUST_STATE.GOHOME;


        });
    }

    public void AttackPlayer()
    {
        print("攻击玩家");
        int tempDamage = -1;

        GameManager.Instance.SetHeartCount(tempDamage);
    }


    public void GoHome()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, GameConfig.CUSTOMER_GOHOME_Z_OFFSET);
        state = CUST_STATE.DISABLE;
        Tween tween =CustomerManager.Instance.CustomerGoHome(this);
        tween.OnComplete(DestroySelf);

    }




    public void MoveWalk(Vector3 pos)
    {
        Tween tween =
        transform.DOMove(pos, GameConfig.CUSTOMER_GOHOME_TIME);
        state = CUST_STATE.WALK;
        transform.position = new Vector3(transform.position.x, transform.position.y, GameConfig.CUSTOMER_WALK_Z_OFFSET);

        tween.OnComplete(OrderFood);//移动到位后开始点餐

    }

    public void FoodAway()
    {

    }
}
