using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {
	
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX; // 그리드 상의 x좌표
    public int gridY;

    public int gCost; // 시작 노드에서 해당 노드까지의 실제 이동거리
    public int hCost; // 해당 노드에서 목표 노드까지의 휴리스틱 추정값
    public Node parent; // 이 노드의 부모 노드 (시작 노드로 부터 이 노드까지 오는 경로 중에 이 노드에 도달하기 직전의 노드)
    int heapIndex;
	
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    /// <summary>
    /// 지금까지의 실제 이동 거리 + 앞으로 남은 거리의 추정값
    /// </summary>
    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    // heap 정렬에 쓰이는 비교 함수
    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}