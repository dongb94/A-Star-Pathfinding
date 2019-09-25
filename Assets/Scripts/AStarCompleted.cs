
using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarCompleted : MonoBehaviour
{
    public Transform StartPoint, EndPoint;

    private Grid grid;

    private void Awake()
    {
        grid = GameObject.Find("Grid").transform.GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath();
    }

    /// <summary>
    /// 길 찾기 함수
    /// </summary>
    private void FindPath()
    {
        var openNode = new Heap<Node>(grid.MaxSize); // cost가 적은 노드부터 처리하기 위해 heap을 사용한다.
        var closeNode = new HashSet<Node>(); // closeNode는 검색에만 사용하므로, 검색 속도가 빠른 HashSet을 쓴다.

        var startNode = grid.NodeFromWorldPoint(StartPoint.position);
        var endNode = grid.NodeFromWorldPoint(EndPoint.position);
        
        openNode.Add(startNode);

        while (openNode.Count != 0)
        {
            var currentNode = openNode.PopFirst(); // 열려있는 노드 중 cost가 가장 작은 노드를 선택한다.
            closeNode.Add(currentNode);

            // 경로를 찾은 경우
            if (currentNode == endNode)
            {
                grid.path = GetPath(startNode, endNode);
                return;
            }

            var neighbourNodes = grid.GetNeighbours(currentNode);

            foreach (var searchNode in neighbourNodes)
            {
                if (!searchNode.walkable || closeNode.Contains(searchNode)) continue;

                int distance = currentNode.gCost + GetDistance(currentNode, searchNode);
                if (distance < searchNode.gCost || !openNode.Contains(searchNode))
                {
                    searchNode.gCost = distance;
                    searchNode.hCost = GetDistance(searchNode, endNode);
                    searchNode.parent = currentNode;
                    
                    if(!openNode.Contains(searchNode))
                        openNode.Add(searchNode);
                }
            }
        }
    }

    /// <summary>
    /// 완성된 경로를 List로 만들어 주는 함수.
    /// 찾아낸 경로는 마지막 노드에서부터 부모노드를 추적하며 만들어 지기 때문에, 역순으로 배열된다.
    /// 이것을 뒤집어주면 올바른 경로가 나온다.
    /// </summary>
    /// <returns>경로를 구성하는 Node의 리스트</returns>
    private List<Node> GetPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node current = endNode;
        while (current != null)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
        
        return path;
    }

    private int GetDistance(Node first, Node second)
    {
        var distX = Math.Abs(first.gridX - second.gridX); // Math.Abs는 절대값을 계산하는 함수.
        var distY = Math.Abs(first.gridY - second.gridY);

        // 가로 세로 방향 거리를 1이라고 했을때 대각선은 루트 2의 거리 
        // (1:1.414) => (1:1.4) = (5:7)
        // 인접한 노드와의 거리 = 5, 대각선 방향에 있는 노드와의 거리 = 7
        if (distX < distY)
            return distX * 7 + (distY - distX) * 5;
        else
            return distY * 7 + (distX - distY) * 5;
    }
}