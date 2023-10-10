using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairRoad : RoadRoot
{


    private LookDir saveDir;

    public override void ConnectRoad()
    {
        
    }

    public override Vector3 GetMovePos()
    {
        return transform.position;
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

}
