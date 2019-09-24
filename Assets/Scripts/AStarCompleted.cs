
using UnityEngine;

public class AStarCompleted : MonoBehaviour
{
    public Transform StartNode, EndNode;

    private Grid grid;

    private void Awake()
    {
        grid = GameObject.Find("Grid").transform.GetComponent<Grid>();
    }
}