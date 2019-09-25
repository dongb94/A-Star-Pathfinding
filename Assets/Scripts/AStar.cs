
using UnityEngine;

public class AStar : MonoBehaviour
{
    public Transform StartPoint, EndPoint;

    private Grid grid;

    private void Awake()
    {
        grid = GameObject.Find("Grid").transform.GetComponent<Grid>();
    }

    private void Update()
    {
        
    }
}