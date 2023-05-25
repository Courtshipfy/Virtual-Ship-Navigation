using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALG_DJ : MonoBehaviour
{
    private List<waypoint> sortList = new List<waypoint>();
    public waypoint searchCenter;
   
    public int manhattan(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
    
    public List<waypoint> GetPath()
    {
        PathFinding.Instance.wayPointDic.TryGetValue(PathFinding.Instance.startPoint,out PathFinding.Instance.startObj);
        PathFinding.Instance.wayPointDic.TryGetValue(PathFinding.Instance.endPoint, out PathFinding.Instance.endObj);

        DJ();
        CreatePath();
        
        PathFinding.Instance.startObj.GetComponent<MeshRenderer>().material.color = Color.cyan;
        PathFinding.Instance.endObj.GetComponent<MeshRenderer>().material.color = Color.cyan;
        return PathFinding.Instance.path;
    }

    private void ExploreAround()
    {
        if (PathFinding.Instance.isRunning == false)
            return;
        
        foreach (var direction in PathFinding.Instance.directions)
        {
            var exploreArounds = searchCenter.GetPosition() + direction;
            
            try
            {
                var neighbour = PathFinding.Instance.wayPointDic[exploreArounds];
                if (neighbour.isExplored || sortList.Contains(neighbour))
                {
                    
                }
                else
                {
                    PathFinding.Instance.wayPointDic[exploreArounds].GetComponent<MeshRenderer>().material.color = Color.green;
                    sortList.Add(neighbour);
                    neighbour.exploredFrom = searchCenter;
                    neighbour.exploredFromPoint = searchCenter.GetPosition();
                }
                PathFinding.Instance.searchCosts++;
            }
            catch
            {
                Debug.LogWarning(exploreArounds + "Not Existed");
            }
        }
    }
    
    public void DJ()
    {
        sortList.Add(PathFinding.Instance.startObj);
        while (sortList.Count > 0)
        {
            sortList.Sort((waypoint a, waypoint b) =>
            {
                return manhattan(PathFinding.Instance.startPoint, a.GetPosition()) - manhattan(PathFinding.Instance.startPoint, b.GetPosition());
            });
            searchCenter = sortList[0];
            sortList.RemoveAt(0);
            Debug.Log("Search from:" + searchCenter.GetPosition());
            
            StopIfSearchEnd();
            ExploreAround();
            searchCenter.isExplored = true;
        }
    }

    public void StopIfSearchEnd()
    {
        if (searchCenter == PathFinding.Instance.endObj)
        {
            PathFinding.Instance.isRunning = false;
            Debug.Log("Stooooooop");
        }
    }

    public void CreatePath()
    {
        PathFinding.Instance.path.Add(PathFinding.Instance.endObj);
        PathFinding.Instance.pathList.Add(PathFinding.Instance.endPoint);
        waypoint prePoint = PathFinding.Instance.endObj.exploredFrom;

        while (prePoint != PathFinding.Instance.startObj)
        {
            prePoint.GetComponent<MeshRenderer>().material.color = Color.yellow;
            PathFinding.Instance.path.Add(prePoint);
            PathFinding.Instance.pathList.Add(prePoint.GetPosition());
            
            prePoint = prePoint.exploredFrom;
        }
        
        PathFinding.Instance.path.Add(PathFinding.Instance.startObj);
        PathFinding.Instance.path.Reverse();
        
        PathFinding.Instance.pathList.Add(PathFinding.Instance.startPoint);
        PathFinding.Instance.pathList.Reverse();
    }
}

