using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairRoad : RoadRoot
{
    public override void ConnectRoad()
    {
        
    }

    public override Vector3 GetMovePos()
    {
        return transform.position;
    }


}
