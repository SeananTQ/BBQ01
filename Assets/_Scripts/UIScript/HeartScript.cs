using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour {

    public List<Transform> iconList;

    public int realHp;
    public int curHp;
    public int maxHp;


    private void Awake()
    {
        maxHp = iconList.Count;
        realHp = maxHp;
        curHp = realHp;
    }


    void Start () {
		
	}

    void Update() {

	}

    public void SetHp(int value)
    {
        realHp = value;


        print("SetHp");


        if (realHp < curHp)
        {
            curHp = realHp;
            for (int i = 0; i < iconList.Count; i++)
            {
                if (i + 1 > realHp)
                {
                    iconList[i].gameObject.SetActive(false);
                }
            }
        }



    }

}
