using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text searchCost;

    public void Update()
    {
        searchCost.text = PathFinding.Instance.searchCosts.ToString();
    }
}
