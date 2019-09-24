using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionAStar : MonoBehaviour
{
    public static DescriptionAStar Instance;

    public int Size;
    public bool isOnFinding;
    
    private AStarNodeButton[][] _nodes;
    private AStarNodeButton start, end;
    
    private void Awake()
    {
        Instance = this;
        isOnFinding = false;
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

    public void StartFindPath()
    {
        isOnFinding = true;
    }

    public void CheckNeighbourNode(AStarNodeButton node)
    {
        for (int y = -1; y <= 1; y++)
        {
            if(node.y == 0 && y==-1) continue;
            if(node.y == Size-1 && y==1) continue;
            for (int x = -1; x <= 1; x++)
            {
                if (y == 0 && x == 0) continue;
                if(node.x == 0 && x==-1) continue;
                if(node.x == Size-1 && x==1) continue;
                var searchNode = _nodes[node.y + y][node.x + x];
                if (searchNode.isClosed || !searchNode.isWalkable) continue;

                int dst = 0, rotation = 0;
                
                switch (y)
                {
                    case -1 :
                        dst = x == 0 ? 5 : 7;
                        rotation = (7 - x) % 8;
                        break;
                    case 0 :
                        dst = 5;
                        rotation = x == -1 ? 1 : 5;
                        break;
                    case 1 :
                        dst = x == 0 ? 5 : 7;
                        rotation = 3 + x;
                        break;
                }

                if (searchNode.Cost == 0)
                {
                    searchNode.Cost = node.Cost + dst;
                    var dstX = Mathf.Abs(end.x - searchNode.x);
                    var dstY = Math.Abs(end.y - searchNode.y);
                    if (dstX < dstY)
                        searchNode.Heuristics = 7 * dstX + 5 * (dstY - dstX);
                    else
                        searchNode.Heuristics = 7 * dstY + 5 * (dstX - dstY);
                    searchNode.SetParentsNode(node, rotation);
                }
                else
                {
                    if (searchNode.Cost <= node.Cost + dst) continue;
                    searchNode.Cost = node.Cost + dst;
                    searchNode.SetParentsNode(node, rotation);
                }
            }
        }
        if(node == end) node.FindThePath();
    }
}
