using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AStar : MonoBehaviour
{

    public int Size;
    
    private AStarNodeButton[][] _nodes;
    
    private void Awake()
    {
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
                button.transform.SetParent(GameObject.Find("Canvas").transform);
                // button.transform.localScale = Vector3.one;
                button.transform.localPosition = new Vector3((j - Size/2f + 0.5f) * 77, (i - Size/2f + 0.5f) * 77, 0);
            }
        }
    }
}
