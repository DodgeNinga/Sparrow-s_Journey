using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField] private RoadRoot currentRoad;

    private PlayerMove playerMove;

    public bool clickAble  = true;

    private void Awake()
    {
        
        playerMove = GetComponent<PlayerMove>();

    }

    private void Update()
    {

        InputChack();

    }


    private void InputChack()
    {

        if(Input.GetMouseButtonDown(0) && clickAble)
        {

            GetClickBox();

        }

    }

    private void GetClickBox()
    {

        var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(cameraRay, out var hit, 1000, LayerMask.GetMask("Road")))
        {

            if(hit.transform.TryGetComponent<RoadRoot>(out var compo))
            {

                clickAble = false;
                FindRoad(compo);

            }

        }

    }

    /// <summary>
    /// 최단거리의 길 탐색
    /// </summary>
    /// <param name="end"></param>
    private void FindRoad(RoadRoot end)
    {

        (RoadRoot road, int idx) endKey = (null, 0);
        Dictionary<(RoadRoot road, int idx), List<RoadRoot>> roadContainer = new();
        Queue<(RoadRoot road, int idx)> visQueue = new(); //방문큐
        HashSet<RoadRoot> visited = new() { currentRoad }; //방문했니?

        CreateConnectKey(currentRoad, visQueue, roadContainer, visited, (null, 0));

        while(visQueue.Count > 0) 
        {

            var current = visQueue.Dequeue();
            var last = roadContainer[current].Last();

            visited.Add(last);

            if(last == end) //도착이라면?
            {

                endKey = current;
                break;

            }
            else if (IsCrossRoad(last, visited)) //교차로라면?
            {

                CreateConnectKey(last, visQueue, roadContainer, visited, current);

            }
            else //아니라면?
            {

                if(GetConnectRoadIndex(last, visited) != -1)
                {

                    roadContainer[current].Add(
                        last.connected[GetConnectRoadIndex(last, visited)].road);
                    visQueue.Enqueue(current);

                }

            }

        }

        if(endKey.road != null)
        {

            currentRoad = roadContainer[endKey].Last();
            playerMove.ExecuteMove(roadContainer[endKey].ToList(), () => { clickAble = true; });

        }
        else
        {

            clickAble = true;

        }


    }

    private void CreateConnectKey(RoadRoot current, 
        Queue<(RoadRoot road, int idx)> visQueue,
        Dictionary<(RoadRoot road, int idx), List<RoadRoot>> roadContainer,
        HashSet<RoadRoot> visited, 
        (RoadRoot road, int idx) beforeKey)
    {

        for(int i = 0; i < current.connected.Count; i++)
        {

            if (visited.Contains(current.connected[i].road) || 
                !current.connected[i].road.moveAble || roadContainer.ContainsKey((current, i))) continue;

            var obj = (current, i);
            roadContainer.Add(obj, new());
            visQueue.Enqueue(obj);

            if(beforeKey.road != null)
            {

                roadContainer[obj] = roadContainer[beforeKey].ToList(); //값만 복사

            }

            roadContainer[obj].Add(current.connected[i].road);

        }

    }

    private bool IsCrossRoad(RoadRoot road, HashSet<RoadRoot> visited)
    {

        return road.connected.Where((x) =>
        {

            return !visited.Contains(x.road) && x.road.moveAble;

        }).ToList().Count > 1;

    }

    private int GetConnectRoadIndex(RoadRoot road, HashSet<RoadRoot> visited)
    {

        return road.connected.FindIndex(x =>
        {

            return x.road.moveAble && !visited.Contains(x.road);

        });

    }

}