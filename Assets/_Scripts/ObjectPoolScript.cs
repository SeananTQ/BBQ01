using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum ObjectNameID
{
    OTHER,
    FOOD_3,//香肠
    FOOD_4,//玉米
    FOOD_1,//豆腐
    FOOD_2,//丸子
    FOOD_5,
    MONEY_1,
    CUSTOMER_1,
}


public class ObjectPoolScript : MonoBehaviour
{

    public static ObjectPoolScript Instance { get; private set; }

    [SerializeField]
    private ObjectPool FOOD_3;
    [SerializeField]
    private ObjectPool FOOD_4;
    [SerializeField]
    private ObjectPool FOOD_1;
    [SerializeField]
    private ObjectPool FOOD_2;
    [SerializeField]
    private ObjectPool FOOD_5;
    [SerializeField]
    private ObjectPool MONEY_1;
    [SerializeField]
    private ObjectPool CUSTOMER_1;

    void Awake()
    {
        Instance = this;
    }



    public Transform GetObject(Transform obj, ObjectNameID name)
    {
        switch (name)
        {
            case ObjectNameID.FOOD_3:
                return FOOD_3.GetObject(obj);
            case ObjectNameID.FOOD_2:
                return FOOD_2.GetObject(obj);
            case ObjectNameID.FOOD_4:
                return FOOD_4.GetObject(obj);
            case ObjectNameID.FOOD_1:
                return FOOD_1.GetObject(obj);
            case ObjectNameID.FOOD_5:
                return FOOD_5.GetObject(obj);
            case ObjectNameID.MONEY_1:
                return MONEY_1.GetObject(obj);
            case ObjectNameID.CUSTOMER_1:
                return CUSTOMER_1.GetObject(obj);
            default: return null;
        }
    }
    public void RecycleObject(Transform obj, ObjectNameID name)
    {
        switch (name)
        {
            case ObjectNameID.FOOD_3:
                FOOD_3.RecycleObject(obj);
                break;
            case ObjectNameID.FOOD_2:
                FOOD_2.RecycleObject(obj);
                break;
            case ObjectNameID.FOOD_4:
                FOOD_4.RecycleObject(obj);
                break;
            case ObjectNameID.FOOD_1:
                FOOD_1.RecycleObject(obj);
                break;
            case ObjectNameID.FOOD_5:
                FOOD_5.RecycleObject(obj);
                break;

            case ObjectNameID.MONEY_1:
                MONEY_1.RecycleObject(obj);
                break;
            case ObjectNameID.CUSTOMER_1:
                CUSTOMER_1.RecycleObject(obj);
                break;
            default:

                // Destroy(obj.gameObject);
                break;
        }
    }

    

    [Serializable]
    public class ObjectPool
    {
        public Stack<Transform> stack;
        public Transform defaultRoot;
        public Transform prefab;
        public int maxCount;
        /// <summary>
        /// 预先创建数量
        /// </summary>
        public int advanceCount;
        public int count;


        public ObjectPool()
        {
            stack = new Stack<Transform>();

        }

        //取得对象
        public Transform GetObject(Transform obj)
        {
            if (count == 0 )
            {
                Transform temp;
                if (obj)
                {
                    //   print("数量不足，创建新的"+obj.name + "         "+ defaultRoot);
                    temp = Instantiate(obj);
                }
                else
                {
                    temp = Instantiate(prefab);
                }

                temp.gameObject.SetActive(true);
                temp.GetComponent<IPoolElement>().InitData();//初始化数据

                if (defaultRoot)
                {
                   
                    temp.SetParent(defaultRoot, true);

                    return temp;
                }
                else
                {
                    return temp;
                }

            }
            else
            {
                Transform temp = stack.Pop();
                count--;
                temp.gameObject.SetActive(true);
                temp.GetComponent<IPoolElement>().InitData();//初始化数据
                return temp;
            }
        }


        public void RecycleObject(Transform obj)
        {
            if (maxCount <= 0 || count < maxCount)
            {
      
                obj.SetParent(defaultRoot, true);
                stack.Push(obj);
                count++;
                obj.gameObject.SetActive(false);
            }
        }



    }


}
