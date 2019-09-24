using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public bool onlyDisplayPathGizmos;
	public Vector2 gridWorldSize; // 그리드의 실제 크기
	public float nodeRadius;// 한 노드의 반지름
	Node[,] grid; // 그리드, 노드를 저장하는 2차원 배열

	float nodeDiameter; // 노드의 지름
	int gridSizeX, gridSizeY; // 가로, 세로에 들어가는 노드의 갯수

	/// <summary>
	/// 길 찾기가 완료 된 후 경로를 저장하는 리스트
	/// </summary>
	public List<Node> path;

	void Start() {
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	/// <summary>
	/// 그리드의 총 노드 갯수를 반환하는 프로퍼티
	/// </summary>
	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	/// <summary>
	/// 그리드를 초기화 하는 함수
	/// </summary>
	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2; // 그리드 왼쪽 아래 꼭지점의 위치

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius)); // collider 충돌 확인을 통해 갈 수 있는 노드와 갈 수 없는 노드를 구분
				grid[x,y] = new Node(walkable,worldPoint, x,y); // 그리드의 각 노드 초기화
			}
		}
	}

	/// <summary>
	/// 기준 노드와 인접한 노드 (최대 8개)를 반환하는 함수
	/// </summary>
	/// <param name="node">기준 노드</param>
	/// <returns>인접 노드의 리스트</returns>
	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	
	/// <summary>
	/// world position을 그리드 상의 좌표 공간으로 변환해 주는 함수 
	/// </summary>
	/// <param name="worldPosition">유니티의 world position</param>
	/// <returns>변환된 좌표에 해당하는 노드</returns>
	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}
	
	/// <summary>
	/// 시각화 함수
	/// 유니티 Scene에 그림을 그려주는 역할
	/// </summary>
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));

		if (onlyDisplayPathGizmos) {
			if (path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
		}
		else {
			if (grid != null) {
				foreach (Node n in grid) {
					Gizmos.color = (n.walkable)?Color.white:Color.red;
					if (path != null)
						if (path.Contains(n))
							Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
		}
	}
}