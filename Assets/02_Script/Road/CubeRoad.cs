using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeRoad : RoadRoot
{

    private LookDir saveDir;

    private Vector3[] rayPoints => new Vector3[]
        {

            transform.right, -transform.right,
            transform.forward, -transform.forward

        };

    private void Awake()
    {

        ConnectRoad();

    }

    private LookDir GetLookDir(Vector3 dir)
    {

        if (dir == transform.forward) return LookDir.front;
        else if (dir == transform.right) return LookDir.right;
        else if (dir == -transform.forward) return LookDir.back;
        else return LookDir.left;

    }

    public override Vector3 GetMovePos()
    {

        return transform.position + transform.up / 2;

    }

    public void SetDir(int dir)
    {

        saveDir = (LookDir)dir;

    }

    public void Connect(RoadRoot road)
    {

        if (connected.Find(x => x.road == road) != null) return;

        connected.Add(new RoadClass
        {

            road = road,
            dir = saveDir

        });

    }

    public void Disconncet(RoadRoot road)
    {

        connected.Remove(connected.Find(x => x.road == road));

    }

    public override void ConnectRoad()
    {

        foreach(var dir in rayPoints)
        {

            if(Physics.Raycast(transform.position, dir, out var hit, 1.3f, LayerMask.GetMask("Road")))
            {

                if(hit.transform.TryGetComponent<RoadRoot>(out var compo))
                {

                    if (connected.Find(x => x.road == compo) != null) continue;

                    connected.Add(new RoadClass {road = compo, dir = GetLookDir(dir)});

                }

            }

        }

    }


#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {

        var old = Gizmos.color;
        Gizmos.color = Color.black;

        foreach(var dir in rayPoints)
        {

            Gizmos.DrawLine(transform.position, transform.position + dir);

        }

        Gizmos.color = old;

    }

#endif

}
