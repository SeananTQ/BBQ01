using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

using UnityEngine.UI;
public class BackGround : MonoBehaviour {
    public Transform tf;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseUp()
    {
    }



    void Test()
    {
        Vector3 vector3 = Input.mousePosition;
        vector3.z = 0;
        Ray ray = Camera.main.ScreenPointToRay(vector3);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        print(" Hit: " + hit.point);




        tf.position = hit.point;

    }
}
