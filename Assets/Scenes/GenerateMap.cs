using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateMap : MonoBehaviour
{
    public static GenerateMap Instance;
    public GameObject prefab;
    public Vector2Int prefabNums;
    public Transform orign;
    public PathFinding pathFinder;
    [Range(1, 9)] public int complexity;
    public List<waypoint> wayPointsCollection = new List<waypoint>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < prefabNums.x; i++)
        {
            for (int j = 0; j < prefabNums.y; j++)
            {
                Vector3 newPos = new Vector3(i * 1.5f, 0, j * 1.5f);
                GameObject newObj = Instantiate(prefab, newPos, Quaternion.identity);
                newObj.transform.SetParent(orign);
                
                wayPointsCollection.Add(newObj.GetComponent<waypoint>());
            }
        }

        int totalNum = prefabNums.x * prefabNums.y;
        int comcomplexityNum = totalNum / 10 * complexity;
        for (int i = 0; i < comcomplexityNum; i++)
        {
            int random = Random.RandomRange(0, totalNum);
            if (wayPointsCollection[random] == PathFinding.Instance.startObj ||
                wayPointsCollection[random] == PathFinding.Instance.endObj)
            {
                
            }
            else
            {
                wayPointsCollection[random].GetComponent<MeshRenderer>().material.color = Color.red;
                wayPointsCollection.RemoveAt(random); 
            }
            
        }
    }

    public void reset()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }
}
