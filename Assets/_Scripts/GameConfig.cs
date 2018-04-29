public static class GameConfig
{
    public static float FOOD_MOVE_TIME=0.3F;
    public static float FOOD_OFFSET_TIME = 0.1F;
    public static float CUSTOMER_GOHOME_TIME = 2;
    public static float CUSTOMER_WALK_TIME = 0.4F;//顾客走路上下浮动的时间
    public static float CUSTOMER_WALK_SHAKE_RANGE = 1;//顾客走路上下颠簸的距离
    public static float CUSTOMER_WALK_Z_OFFSET = 10;
    public static float CUSTOMER_GOHOME_Z_OFFSET = 20;

    public static int FOOD_ORDER = 500;
    public static int FOOD_ORDER_UP = 500;
    public static int FOOD_ORDER_DOWN = 500;


    public static float PATIENCE_ATTACK=200;


    public static class SceneName
    {
        public static string mainTest = "mainTest";
    }
    

    public enum FOOD_STATE
    {
        RAW,    //生肉
        MEDIUM, //半生不熟
        PERFECT, //恰到好处，完美
        TOO,    //烤老了
        BURNT,  //烤糊了
    }
    public enum FOOD_TYPE
    {
        FOOD_3,//香肠
        FOOD_4,//玉米
        FOOD_1,//豆腐
        FOOD_2//丸子
    }
    public enum FOOD_SAUCE
    {
        NONE, //不要酱
        SAUCE_1,//辣酱
        SAUCE_2,//咖喱酱
        BOTH//辣酱和咖喱酱
    }



}
public enum SAUCE_TYPE
{
    SAUCE_1,//辣酱
    SAUCE_2,//咖喱        
}

public enum CUST_STATE
{
    IDLE,
    WALK,
    ORDER,//点餐中
    ANGRY,//生气发怒中
    GOHOME,//准备回家
    DISABLE,//不可使用
}

public enum GAME_MODEL
{
    STAGE,




}

public enum GAME_STATE
{
    LOAD,
    PLAY,
    PUSE,


}
