using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {


    public GameObject arrowUp { get; private set; }
    public GameObject arrowDown { get; private set; }
    public GameObject arrowBack { get; private set; }
    public static MenuManager inst;

    public float scrollSpeed;
    public float lineSpaceing;
    public Node topNode;

    [Serializable]
    public struct Node
    {
        public string context;
        public List<Node> subNodes;
             
    }

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        MakeArrow(Vector3.right, 0, true);
        arrowUp = MakeArrow(Vector3.up, 90);
        arrowDown = MakeArrow(Vector3.down, -90);
        arrowBack = MakeArrow(Vector3.left, 180);
        
        MakeMenu(topNode).Show();
    }

    private GameObject MakeArrow(Vector3 pos, float rotateZ, bool isBold = false)
    {
        TextMesh tm = MakeTextMesh(">");
        tm.color = Color.black;
        Transform tf = tm.transform;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.fontStyle = isBold ? FontStyle.Bold : FontStyle.Normal;
        tf.localPosition = pos + Vector3.left*2;
        tf.SetParent(this.transform);
        tf.Rotate(0, 0, rotateZ);

        return tm.gameObject;
    }

    private TextMesh MakeTextMesh(string text)
    {
        GameObject go = new GameObject();
        go.layer = 15;//menu
        go.name = text;
        TextMesh tm = go.AddComponent<TextMesh>();
        tm.text = text;
        tm.anchor = TextAnchor.MiddleLeft;
        return tm;
    }

    private Menu MakeMenu(Node node)
    {
        GameObject go = new GameObject();
        go.layer = 15;//menu
        go.name = "top Menu";
        go.transform.SetParent(this.transform);
        Menu menu = go.AddComponent<Menu>();
        menu.subMenu = new Menu[node.subNodes.Count];
        go.SetActive(false);


        for(int i =0; i < node.subNodes.Count; i++)
        {
            TextMesh tm = MakeTextMesh(node.subNodes[i].context);
            tm.transform.SetParent(go.transform);
            menu.textMeshs.Add(tm);
            tm.transform.localPosition = new Vector3(0, -tm.transform.GetSiblingIndex() * lineSpaceing, 0);

            if (node.subNodes[i].subNodes.Count > 0)
            {
                Menu subMenu = MakeMenu(node.subNodes[i]);
                
                subMenu.gameObject.name = node.subNodes[i].context;
                subMenu.upperMenu = menu;
                menu.subMenu[i] =subMenu;
            }
        }


        return menu;
    }
}
