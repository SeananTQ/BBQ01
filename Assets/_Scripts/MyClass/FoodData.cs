using System;
using System.Collections.Generic;

public class FoodData : ICloneable
{

 
    public GameConfig.FOOD_SAUCE sauce;
    public GameConfig.FOOD_STATE state;

    public float hp;

     public  int id;
     public  string foodName;
     public  GameConfig.FOOD_TYPE foodType;
     public  int cost_raw;
     public  int cost_none;
     public  int cost_one;
     public  int cost_two;
     public  float maxHp;
     public  int mediumHp;
     public  int perfectHp;
     public  int tooHp;
     public  int burntHp;



    public static   Dictionary<int,FoodData>   ReadTable(List<Dictionary<string, string>>  table )
    {
        Dictionary<int, FoodData> foodDataDict=new Dictionary<int, FoodData>();

        foreach (Dictionary<string, string> tempTable in table)
        {
            Dictionary<string, string> tempData = tempTable;

            FoodData foodData = new FoodData
            {
                id = int.Parse(tempData["id"]),
                foodName = tempData["foodName"],
                foodType = (GameConfig.FOOD_TYPE)Enum.Parse(typeof(GameConfig.FOOD_TYPE), tempData["foodType"]),
                cost_raw = int.Parse(tempData["cost_raw"]),
                cost_none = int.Parse(tempData["cost_none"]),
                cost_one = int.Parse(tempData["cost_one"]),
                cost_two = int.Parse(tempData["cost_two"]),
                maxHp = float.Parse(tempData["maxHp"]),
                mediumHp = int.Parse(tempData["mediumHp"]),
                perfectHp = int.Parse(tempData["perfectHp"]),
                tooHp = int.Parse(tempData["tooHp"]),
                burntHp = int.Parse(tempData["burntHp"])
            };

             foodDataDict.Add(foodData.id,foodData);
        }

        //int id;
        //string foodName;
        //FOOD_TYPE foodType;
        //int cost_raw;
        //int cost_none;
        //int cost_one;
        //int cost_two;
        //float maxHp;
        //int mediumHp;
        //int perfectHp;
        //int tooHp;
        //int burntHp;



        return foodDataDict;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
