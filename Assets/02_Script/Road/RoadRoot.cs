using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookDir
{

    front, back, left, right,

}

[System.Serializable]
public class RoadClass
{

    public RoadRoot road;
    public LookDir dir;

}

public abstract class RoadRoot : MonoBehaviour
{
    /// <summary>
    /// 연결된 모든 길
    /// </summary>
    [field: SerializeField] public List<RoadClass> connected { get; protected set; } = new();

    /// <summary>
    /// 이동 가능한가?
    /// </summary>
    public bool moveAble = true;

    /// <summary>
    /// 오브젝트에 이동 가능한 좌표 반환
    /// </summary>
    /// <returns></returns>
    public abstract void ConnectRoad();
    public abstract Vector3 GetMovePos();

#if UNITY_EDITOR

    protected virtual void OnDrawGizmos()
    {

        var old = Gizmos.color;

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(GetMovePos(), 0.2f);

        Gizmos.color = old;

    }

#endif

}