using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ObjectAutoRenameScript  ))]

public class ObjectAutoRenameEditor : Editor {

    public List<Transform > objectList;

    Transform transform;
    string baseName;

    private void OnEnable()
    {
        string temp;
        transform = ((ObjectAutoRenameScript)target).transform;
        temp= transform.GetChild(0).name;
        baseName = temp.Substring(0,temp.IndexOf("_")+1);

        objectList = new List<Transform>();
        foreach (Transform tf in transform)
        {
            objectList.Add(tf);
        }
        
    }

    //private void OnDisable()
    //{
    //    objectList.Clear();
    //}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("以下为 编辑器脚本内容");
        EditorGUILayout.Space();
        GUILayout.Label("将以 "+baseName+ " 开头，重命名"+objectList.Count+"个子物体");
       // GUILayout.TextField(baseName);
        
      bool tempBool=   GUILayout.Button("Format Child");
        if(tempBool)
        {
            for (int i=0; i < objectList.Count; i++)
            {
                objectList[i].name = baseName + (i+1);

                Text text= objectList[i].GetComponentInChildren<Text>();
                text.text =""+( i + 1);
            }

            GUILayout.Label("重命名完毕 ");

          Debug.Log("重命名完毕");
        }
    }
}
