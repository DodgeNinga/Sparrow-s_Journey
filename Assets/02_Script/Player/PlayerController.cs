using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [field:SerializeField] public RoadRoot currentRoad { get; private set; }

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
    /// �ִܰŸ��� �� Ž��
    /// </summary>
    /// <param name="end"></param>
    private void FindRoad(RoadRoot end)
    {

        (RoadRoot road, int idx) endKey = (null, 0);
        Dictionary<(RoadRoot road, int idx), List<RoadClass>> roadContainer = new();
        Queue<(RoadRoot road, int idx)> visQueue = new(); //�湮ť
        HashSet<RoadRoot> visited = new() { currentRoad }; //�湮�ߴ�?

        CreateConnectKey(currentRoad, visQueue, roadContainer, visited, (null, 0));

        while(visQueue.Count > 0) 
        {

            var current = visQueue.Dequeue();
            var last = roadContainer[current].Last();

            visited.Add(last.road);

            if(last.road == end) //�����̶��?
            {

                endKey = current;
                break;

            }
            else if (IsCrossRoad(last.road, visited)) //�����ζ��?
            {

                CreateConnectKey(last.road, visQueue, roadContainer, visited, current);

            }
            else //�ƴ϶��?
            {

                if(GetConnectRoadIndex(last.road, visited) != -1)
                {

                    roadContainer[current].Add(
                        last.road.connected[GetConnectRoadIndex(last.road, visited)]);
                    visQueue.Enqueue(current);

                }

            }

        }

        if(endKey.road != null)
        {

            currentRoad = roadContainer[endKey].Last().road;
            playerMove.ExecuteMove(roadContainer[endKey].ToList(), () => { clickAble = true; });

        }
        else
        {

            clickAble = true;

        }


    }

    private void CreateConnectKey(RoadRoot current, 
        Queue<(RoadRoot road, int idx)> visQueue,
        Dictionary<(RoadRoot road, int idx), List<RoadClass>> roadContainer,
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

                roadContainer[obj] = roadContainer[beforeKey].ToList(); //���� ����

            }

            roadContainer[obj].Add(current.connected[i]);

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