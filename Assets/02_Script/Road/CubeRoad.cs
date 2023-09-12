using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeRoad : RoadRoot
{

    private Vector3[] rayPoints => new Vector3[]
        {

            transform.right, -transform.right,
            transform.forward, -transform.forward

        };

    private void Awake()
    {

        ConnectRoad();

    }

    public override Vector3 GetMovePos()
    {

        return transform.position + transform.up / 2;

    }

    public override void ConnectRoad()
    {



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
