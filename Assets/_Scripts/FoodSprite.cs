using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FoodSprite : MonoBehaviour, IPoolElement
{

    [Header("Data")]
    public string foodName;

    public int foodID;

    public Animator animator;

    public FoodData foodData = new FoodData();

    //拖拽时鼠标偏移量
    private Vector3 mouseOffset = Vector3.zero;

    //当前所在容器
    public IFoodHolder CurrentfoodHolder;


    public bool IsMoving = false;
    public void BeginMove() { IsMoving = true; }
    public void FinshMoving() { IsMoving = false; }


    public bool onClip = false;

    public Transform clipTransform;
    public Transform saucePos_spicy;
    public Transform saucePos_curry;

    public bool IsIntact
    {
        get
        {
            if (foodData.maxHp - foodData.hp < Mathf.Epsilon)
            {
                return true;
            }
            else
                return false;
        }
    }


    void Start()
    {
        foodData = (FoodData) GameManager.Instance.foodDataBase[foodID].Clone();




        InitData();

    }



    public void InitData()
    {
        foodData.state = GameConfig.FOOD_STATE.RAW;
        animator = transform.GetComponent<Animator>();
        animator.SetInteger("State", 0);
        foodData.maxHp = 30;
        foodData.hp = foodData.maxHp;

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        //transform.position = Vector3.one;
    }

    public void InitData(FoodData foodData)
    {
        switch (foodData.foodType)
        {
            case GameConfig.FOOD_TYPE.FOOD_3:

                break;
        }

        switch (foodData.sauce)
        {
            case GameConfig.FOOD_SAUCE.NONE:

                break;

        }

        switch (foodData.state)
        {
            case GameConfig.FOOD_STATE.RAW:

                break;

        }

        animator = transform.GetComponent<Animator>();
        animator.SetInteger("State", 0);
    }


    // Update is called once per frame
    void Update()
    {




        RefreshImage();

    }

    public bool CanMove()
    {
        return true;
    }

    public void MoveToPosition(Transform tf)
    {
        if (tf)
        {

            transform.position = new Vector3(tf.position.x, tf.position.y, transform.position.z);


        }
    }

    public bool HeatFood(float damage)
    {
        foodData.hp -= damage;
        if (foodData.hp <= 0f)
        {
            foodData.state = GameConfig.FOOD_STATE.BURNT;
            return false;
        }
        //生的
        if (foodData.hp / foodData.maxHp > 0.9f)
        {
            foodData.state = GameConfig.FOOD_STATE.RAW;

        }
        //半生不熟
        else if (foodData.hp / foodData.maxHp > 0.7f)
        {
            foodData.state = GameConfig.FOOD_STATE.MEDIUM;

        }
        else if (foodData.hp / foodData.maxHp > 0.5f)
        {
            foodData.state = GameConfig.FOOD_STATE.PERFECT;

        }
        else if (foodData.hp / foodData.maxHp > 0.3f)
        {
            foodData.state = GameConfig.FOOD_STATE.TOO;

        }
        else if (foodData.hp / foodData.maxHp > 0.1f)
        {
            foodData.state = GameConfig.FOOD_STATE.BURNT;

        }
        
        return true;
    }

    public bool AddSauce(SAUCE_TYPE type)
    {

        switch (foodData.sauce)
        {
            case GameConfig.FOOD_SAUCE.BOTH:
                return false;
            case GameConfig.FOOD_SAUCE.NONE:
                if (type == SAUCE_TYPE.SAUCE_2)
                {
                    saucePos_curry.gameObject.SetActive(true);
                    foodData.sauce = GameConfig.FOOD_SAUCE.SAUCE_2;
                }
              else   if (type == SAUCE_TYPE.SAUCE_1)
                {
                    saucePos_spicy.gameObject.SetActive(true);
                    foodData.sauce = GameConfig.FOOD_SAUCE.SAUCE_1;
                }
                break;
            case GameConfig.FOOD_SAUCE.SAUCE_1:
                if (type == SAUCE_TYPE.SAUCE_2)
                {
                    saucePos_curry.gameObject.SetActive(true);
                    foodData.sauce = GameConfig.FOOD_SAUCE.BOTH;
                }
                break;
            case GameConfig.FOOD_SAUCE.SAUCE_2:
                if (type == SAUCE_TYPE.SAUCE_1)
                {
                    saucePos_spicy.gameObject.SetActive(true);
                    foodData.sauce = GameConfig.FOOD_SAUCE.BOTH;
                }
                break;

        }
        

        return false;
    }




    private void RefreshImage()
    {
        switch (foodData.state)
        {
            case GameConfig.FOOD_STATE.RAW:
                animator.SetInteger("State", 0);
                break;
            case GameConfig.FOOD_STATE.MEDIUM:
                animator.SetInteger("State", 1);
                break;
            case GameConfig.FOOD_STATE.PERFECT:
                animator.SetInteger("State", 2);
                break;

            case GameConfig.FOOD_STATE.TOO:
                animator.SetInteger("State", 3);
                break;
            case GameConfig.FOOD_STATE.BURNT:
                animator.SetInteger("State", 4);

                break;
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }


    //private void OnMouseUp()
    //{
    //    FoodController.Instance.PickFood(this);
    //}

    public void DestroySelf()
    {
        //  StartCoroutine(WaitToDestroy(0.3f));
        //  CurrentfoodHolder.FoodAway();

        switch (foodData.foodType)
        {
            case GameConfig.FOOD_TYPE.FOOD_1:
                ObjectPoolScript.Instance.RecycleObject(this.transform, ObjectNameID.FOOD_1);
                break;
            case GameConfig.FOOD_TYPE.FOOD_2:
                ObjectPoolScript.Instance.RecycleObject(this.transform, ObjectNameID.FOOD_2);
                break;
            case GameConfig.FOOD_TYPE.FOOD_3:
                ObjectPoolScript.Instance.RecycleObject(this.transform, ObjectNameID.FOOD_3);
                break;
            case GameConfig.FOOD_TYPE.FOOD_4:
                ObjectPoolScript.Instance.RecycleObject(this.transform, ObjectNameID.FOOD_4);
                break;

        }


   
    }

    //IEnumerator WaitToDestroy(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    ObjectPoolScript.Instance.RecycleObject(this.transform , ObjectNameID.FOOD_1);
    //    //   print("销毁食物，同时播放了丢垃圾的声音");
    //    //   print("扣钱");
    //}
    private void OnMouseDown()
    {

        if (IsMoving)
        {
            print("还在移动中，无法被操作");
            return;
        }
        mouseOffset = transform.position - GameManager.Instance.GetMousePosition();
        PickFood();
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

    private void PickFood()
    {
        //加一些特效，例如放大、外发光，或者是有夹子
        GetComponent<SpriteRenderer>().sortingOrder = GameConfig.FOOD_ORDER_UP;
        //  onClip = true;//夹子
        clipTransform.gameObject.SetActive(true);
        //然后通知其拥有者，食物被拿起来了
        CurrentfoodHolder.FoodAway();

      //  print(foodData.foodName+" "+foodData.foodType);
    }

    private void OnMouseUp()
    {
      
        GetComponent<SpriteRenderer>().sortingOrder = GameConfig.FOOD_ORDER;
        Collider2D[] re = new Collider2D[3];
        transform.GetComponent<Collider2D>().OverlapCollider(GameManager.Instance.contactFilter2D, re);
        //onClip = false;//夹子
        clipTransform.gameObject.SetActive(false);


        if (re[0])
        {
            IFoodHolder tempHolder = re[0].transform.GetComponent<IFoodHolder>();

            if (tempHolder == null)
            {
                //如果碰到的目标不是能拿食物的东西，那么就把食物返回原处。
                // transform.DOMove(CurrentfoodHolder.GetFoodPositionTransform().position, GameConfig.FOOD_MOVE_TIME);
                CurrentfoodHolder.TakeFood(this);

                print("目标不在正确");
            }
            else
            {
                if (tempHolder.TakeFood(this))
                {
                    //如果目标物体成功的拿了食物，那么就更新数据
                    //  CurrentfoodHolder.FoodAway();
                    CurrentfoodHolder = tempHolder;

                }
                else
                {
                    //如果碰到的目标不能拿食物，那么就把食物返回原处。
                    // transform.DOMove(CurrentfoodHolder.GetFoodPositionTransform().position, GameConfig.FOOD_MOVE_TIME);
                    CurrentfoodHolder.TakeFood(this);

                }
            }

        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  print("OnTriggerEnter2D" + transform.name);
    }


}
