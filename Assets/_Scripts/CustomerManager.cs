using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerManager : MonoBehaviour {

    public List<Transform> customerPosition;

    public List<Transform> customerPrefab;

    public static CustomerManager Instance { get; private set; }

    public List<bool> isPositionIdel;

    public List<Transform> customerHomePos;

    float addCustomerRate = 3f;
    private float currentTime=0f;

    int curCustCount;//当前顾客数量
    int maxCustCount = 0;//本关最大顾客数量

    void Awake()
    {
        Instance = this;
    }


    void Start () {
        isPositionIdel = new List<bool>();
        isPositionIdel.Add(true);
        isPositionIdel.Add(true);
        isPositionIdel.Add(true);

        maxCustCount = GameManager.Instance.stageData.customerCount;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.Instance.gameState == GAME_STATE.PLAY)
        {
            if (maxCustCount > 0)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= addCustomerRate)
                {
                    if (HaveEmptyPosition)
                    {
                        currentTime = 0;
                        AddCustomer();
                    }
                    else
                    {
                        //特殊玩法需要判断是否有强制增加的剧情顾客

                        //减少一些时间，然后继续累计
                        currentTime *= 0.6f;

                    }
                }

            }
            else
            {
                //顾客已出完，可以通知判断游戏是否结束

            }
        }       

    }

    public bool HaveEmptyPosition {
        get {
            for (int i = 0; i < isPositionIdel.Count; i++)
            {
                if (isPositionIdel[i])
                {
                    return true;
                }
            }
            return false;
        }

    }


    void AddCustomer()
    {
        maxCustCount--;
        GameManager.Instance.SetCustomerCount(maxCustCount);

        List<Transform> tempList=new List<Transform>();
        //丑陋的代码
        for (int i = 0; i < isPositionIdel.Count; i++)
        {
            if (isPositionIdel[i])
            {
                tempList.Add(customerPosition[i]);
            }
        }
        int tempInt = Random.Range(0, tempList.Count);
        Transform  tempTf= tempList[tempInt];

        //查找被随机到的POS在List中对应的位置，然后更改isPositionIdel
        int tempIndex = customerPosition.IndexOf(tempList[tempInt]);
        isPositionIdel[tempIndex] = false;
       // print("=====" + tempInt+"     "+ tempTf.name);

        Transform tempCustomer = ObjectPoolScript.Instance.GetObject(null, ObjectNameID.CUSTOMER_1);
        tempCustomer.SetParent(tempTf);
        tempCustomer.position = customerHomePos[Random.Range(0, customerHomePos.Count)].position;

        tempCustomer.GetComponent<Customer>().MoveWalk(tempTf.position);

        //Tween tween=
        //tempCustomer.DOMove(tempTf.position, GameConfig.CUSTOMER_GOHOME_TIME);
        //tween.OnComplete(tempCustomer.GetComponent<Customer>().OrderFood);//移动到位后开始点餐


        tempCustomer.GetComponent<Customer>().index = tempIndex;
    }

    public void SubtractCustomer(Customer cust)
    {
        isPositionIdel[cust.index] = true;  

    }

    public void Payment(int index )
    {
        Transform tempMoney = ObjectPoolScript.Instance.GetObject(null, ObjectNameID.MONEY_1);
        tempMoney.SetParent(customerPosition[index]);
        tempMoney.position = customerPosition[index].position;
        
    }


    public void CustomerGoHome(Customer cust)
    {
        //随机一个回家方向
        Transform tempTf = customerHomePos[Random.Range(0, customerHomePos.Count)];
        if (tempTf)
        {

            Tween tween=
          cust.transform.DOMove(tempTf.position,GameConfig.CUSTOMER_GOHOME_TIME);

            //在队列中移除该顾客
            SubtractCustomer(cust);

        }
    }
}
