using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData
{

    public int customerCount;

    public int targetGoldCount;

    public int[] starGold;

    public StageData()
    {
        customerCount = 18;
        targetGoldCount = 700;
        starGold = new int[3];

        starGold[0] = (int)(targetGoldCount * 0.4f);
        starGold[1] = (int)(targetGoldCount * 0.7f);
        starGold[2] = targetGoldCount;
    }

}
