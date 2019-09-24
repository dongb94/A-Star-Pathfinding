using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class AStar : MonoBehaviour
{
    public static AStar Instance;

    public int Size;
    
    private AStarNodeButton[][] _nodes;
    private AStarNodeButton start, end;
    
    private void Awake()
    {
        Instance = this;
        var btnObj = Resources.Load("Button");
        _nodes = new AStarNodeButton[Size][];
        for(int i=0;i < Size; i++)
        {
            _nodes[i] = new AStarNodeButton[Size];
            for (int j = 0; j < Size; j++)
            {
                var button = Instantiate(btnObj) as GameObject;
                var node = button.GetComponent<AStarNodeButton>();
                _nodes[i][j] = node;
                node.x = j;
                node.y = i;
                button.transform.SetParent(GameObject.Find("Canvas").transform);
                // button.transform.localScale = Vector3.one;
                button.transform.localPosition = new Vector3((j - Size/2f + 0.5f) * 77, (i - Size/2f + 0.5f) * 77, 0);
            }
        }
    }

    public bool isSetStart => start != null;
    public bool isSetEnd => end != null;

    public void SetStartAndEnd(AStarNodeButton button)
    {
        if (start == null) start = button;
        else end = button;
    }
    
}
