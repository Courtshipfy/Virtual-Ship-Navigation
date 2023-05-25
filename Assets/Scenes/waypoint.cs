using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class waypoint : MonoBehaviour
{
    public bool isExplored;
    public waypoint exploredFrom;
    public Vector2Int exploredFromPoint;
    public TMP_Text _testText;
    private void Start()
    {
        _testText.text = Mathf.RoundToInt(transform.position.x / 1.5f).ToString() 
                         + "," + 
                         Mathf.RoundToInt(transform.position.z / 1.5f).ToString();
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / 1.5f),
            Mathf.RoundToInt(transform.position.z / 1.5f)
        );
    }
}
