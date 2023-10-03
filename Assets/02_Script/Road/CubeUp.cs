using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeUp : CubeRoad
{

    public override Vector3 GetMovePos()
    {

        return transform.position + Vector3.up / 2;

    }

}
