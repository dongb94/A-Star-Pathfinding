
using System;
using UnityEngine;
using UnityEngine.UI;

public class AStarNodeButton : MonoBehaviour
{
    [NonSerialized] public AStarNodeButton parents;
    [NonSerialized] public bool isWalkable;
    [NonSerialized] public bool isClosed;
    [NonSerialized] public int x, y;

    private int cost, heuristics, totalCost;
    private bool isEndNode;

    private Image _UIImage;
    private Text _costText, _heuristicsText, _totalText;

    private Image[] _arrowGroup;

    public void OnClick()
    {
        if (DescriptionAStar.Instance.isOnFinding)
        {
            if (isClosed || !isWalkable) return;
            if (!isEndNode && cost == 0) return;
            DescriptionAStar.Instance.CheckNeighbourNode(this);
            isClosed = true;
            if (isEndNode) return;
            _UIImage.color = Color.red;
            return;
        }
        
        if (isEndNode) return;
        if (!DescriptionAStar.Instance.isSetEnd)
        {
            isEndNode = true;
            _totalText.text = !DescriptionAStar.Instance.isSetStart ? "START" : "END";
            DescriptionAStar.Instance.SetStartAndEnd(this);
            _UIImage.color = new Color(0,128/255f,1f);
            return;
        }
        isWalkable = !isWalkable;
        _UIImage.color = isWalkable?Color.white:Color.black;
    }

    public void FindThePath()
    {
        _UIImage.color = new Color(0,128/255f,1f);
        if(parents!=null) parents.FindThePath();
    }

    public void SetParentsNode(AStarNodeButton parentsNode, int localRotation)
    {
        if (isClosed) return;
        parents = parentsNode;
        foreach (var arrow in _arrowGroup)
        {
            arrow.color = new Color(0f,.25f,.5f,0);
        }
        _arrowGroup[localRotation].color = new Color(0f,.25f,.5f,1f);
        if (isEndNode) return;
        _UIImage.color = Color.green;
    }

    private void Awake()
    {
        _UIImage = GetComponent<Image>();
        _costText = transform.Find("Cost").GetComponent<Text>();
        _heuristicsText = transform.Find("Heuristics").GetComponent<Text>();
        _totalText = transform.Find("Sum").GetComponent<Text>();
        _arrowGroup = new Image[8];
        _arrowGroup[0] = transform.Find("ArrowGroup").transform.Find("1").GetComponent<Image>();
        _arrowGroup[1] = transform.Find("ArrowGroup").transform.Find("3").GetComponent<Image>();
        _arrowGroup[2] = transform.Find("ArrowGroup").transform.Find("5").GetComponent<Image>();
        _arrowGroup[3] = transform.Find("ArrowGroup").transform.Find("6").GetComponent<Image>();
        _arrowGroup[4] = transform.Find("ArrowGroup").transform.Find("7").GetComponent<Image>();
        _arrowGroup[5] = transform.Find("ArrowGroup").transform.Find("9").GetComponent<Image>();
        _arrowGroup[6] = transform.Find("ArrowGroup").transform.Find("11").GetComponent<Image>();
        _arrowGroup[7] = transform.Find("ArrowGroup").transform.Find("12").GetComponent<Image>();
        foreach (var arrow in _arrowGroup)
        {
            arrow.color = new Color(1f,1f,1f,0);
        }
        isWalkable = true;
        isEndNode = false;
        isClosed = false;
    }
    
    public int Cost
    {
        get { return cost;}
        set
        {
            cost = value;
            TotalCost = heuristics + cost;
            _costText.text = value.ToString();
        }
    }

    public int Heuristics
    {
        get { return heuristics; }
        set
        {
            heuristics = value;
            TotalCost = heuristics + cost;
            _heuristicsText.text = value.ToString();
        }
    }

    public int TotalCost
    {
        get { return totalCost; }
        set
        {
            totalCost = value;
            _totalText.text = value.ToString();
        }
    }

    public void SetColor(Color color)
    {
        _UIImage.color = color;
    }
}