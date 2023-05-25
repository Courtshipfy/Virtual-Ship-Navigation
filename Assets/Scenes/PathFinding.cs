using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance;
    public GameObject ship;
    [SerializeField] public Vector2Int startPoint, endPoint;
    public waypoint startObj, endObj;
    public Dictionary<Vector2Int, waypoint> wayPointDic = new Dictionary<Vector2Int, waypoint>();
    public List<Vector2Int> pathList = new List<Vector2Int>();
    public List<waypoint> path = new List<waypoint>();

    public int searchCosts = 0;

    public Vector2Int[] directions =
    {
        Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
    };
    
    [SerializeField] public bool isRunning = true;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadAllWayPoints();
    }

    public void BFS()
    {
        ALG_BFS bfs = new ALG_BFS();
        bfs.GetPath();
        ship.transform.position = new Vector3(PathFinding.Instance.startPoint.x,1,PathFinding.Instance.startPoint.y);
        StartCoroutine(FindWays(path));
    }

    public void DJ()
    {
        ALG_DJ dj = new ALG_DJ();
        dj.GetPath();
        ship.transform.position = new Vector3(PathFinding.Instance.startPoint.x,1,PathFinding.Instance.startPoint.y);
        StartCoroutine(FindWays(path));
    }

    public void A()
    {
        ALG_A a = new ALG_A();
        a.GetPath();
        ship.transform.position = new Vector3(PathFinding.Instance.startPoint.x,1,PathFinding.Instance.startPoint.y);
        StartCoroutine(FindWays(path));
    }
    
    private void LoadAllWayPoints()
    {
        var wayPoints = GenerateMap.Instance.wayPointsCollection;
        foreach (var waypoint in wayPoints)
        {
            var tempWayPoint = waypoint.GetPosition();
            if (wayPointDic.ContainsKey(tempWayPoint))
            {
                Debug.Log("Skip overlap");
            }
            else
            {
                wayPointDic.Add(tempWayPoint,waypoint);
            }
        }
    }

    IEnumerator FindWays(List<waypoint> _pathwaypoints)
    {
        foreach (var waypoint in _pathwaypoints)
        {
            ship.transform.DOMove((waypoint.transform.position + new Vector3(0, 1, 0)),5,false);

            Vector3 targetDir = waypoint.transform.position - ship.transform.position;
            float angle = Vector3.Angle(targetDir,transform.forward);
            //ship.transform.DORotate(targetDir,5,RotateMode.Fast);

            //ship.transform.DORotate((-waypoint.transform.position + new Vector3(0, 1, 0)),5,RotateMode.Fast);
            //ship.transform.DOLookAt(-(waypoint.transform.position + new Vector3(0, 1, 0)),5,AxisConstraint.Y,Vector3.up);
            //ship.transform.position = waypoint.transform.position + new Vector3(0, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
