
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

    private void Awake()
    {
        _UIImage = GetComponent<Image>();
        _costText = transform.Find("Cost").GetComponent<Text>();
        _heuristicsText = transform.Find("Heuristics").GetComponent<Text>();
        _totalText = transform.Find("Sum").GetComponent<Text>();
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