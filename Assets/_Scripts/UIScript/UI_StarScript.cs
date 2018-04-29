using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UI_StarScript : MonoBehaviour {

    public RectTransform self;
    public  RectTransform image;

    private bool  isLight=false;

    public bool test = false;
    private void Awake()
    {
        self = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start () {
        self.DOScale(new Vector3(0.5f, 0.5f, 1), 0);
    }
	
	// Update is called once per frame
	void Update () {
      //  SetLight(test);

    }

    public void SetLight(bool value)
    {
        if (isLight != value)
        {
            isLight = value;
            if (isLight)
            {
          
                image.gameObject.SetActive(true);
                self.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack);
            }
            else {

                image.gameObject.SetActive(false);

                self.DOScale(new Vector3(0.5f, 0.5f, 1),0.5f).SetEase(Ease.InBack);
            }



        }

    }

}
