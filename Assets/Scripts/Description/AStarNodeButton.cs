
using System;
using UnityEngine;
using UnityEngine.UI;

public class AStarNodeButton : MonoBehaviour
{
    [NonSerialized] public AStarNodeButton parents;
    [NonSerialized] public bool isWalkable;

    private int cost, heuristics, totalCost;

    private Image _UIImage;
    private Text _costText, _heuristicsText, _totalText;

    private Image[] _arrowGroup;

    public void OnClick()
    {
        isWalkable = !isWalkable;
        _UIImage.color = isWalkable?Color.white:Color.black;
    }

    public void FindThePath()
    {
        _UIImage.color = Color.green;
        if(parents!=null) parents.FindThePath();
    }

    public void SetParentsNode(AStarNodeButton parentsNode, int localRotation)
    {
        parents = parentsNode;
        foreach (var arrow in _arrowGroup)
        {
            arrow.color = new Color(255,255,255,0);
        }
        _arrowGroup[localRotation].color = Color.white;
    }

    private void Awake()
    {
        _UIImage = GetComponent<Image>();
        _costText = transform.Find("Cost").GetComponent<Text>();
        _heuristicsText = transform.Find("Heuristics").GetComponent<Text>();
        _totalText = transform.Find("Sum").GetComponent<Text>();
        _arrowGroup = new Image[8];
        _arrowGroup[0] = transform.Find("1").GetComponent<Image>();
        _arrowGroup[1] = transform.Find("3").GetComponent<Image>();
        _arrowGroup[2] = transform.Find("5").GetComponent<Image>();
        _arrowGroup[3] = transform.Find("6").GetComponent<Image>();
        _arrowGroup[4] = transform.Find("7").GetComponent<Image>();
        _arrowGroup[5] = transform.Find("9").GetComponent<Image>();
        _arrowGroup[6] = transform.Find("11").GetComponent<Image>();
        _arrowGroup[7] = transform.Find("12").GetComponent<Image>();
        foreach (var arrow in _arrowGroup)
        {
            arrow.color = new Color(0,0,0,0);
        }
    }
    
    public int Cost
    {
        get { return cost;}
        set
        {
            cost = value;
            _costText.text = value.ToString();
        }
    }

    public int Heuristics
    {
        get { return heuristics; }
        set
        {
            heuristics = value;
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
}