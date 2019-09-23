using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private AStarNodeButton[][] _nodes;
    
    private void Awake()
    {
        _nodes = new AStarNodeButton[10][];
    }
}
