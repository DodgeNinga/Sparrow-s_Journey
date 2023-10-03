using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateEvent : MonoBehaviour
{

    [SerializeField] private float angle;
    [SerializeField] private UnityEvent tEvt;
    [SerializeField] private UnityEvent fEvt;

    public void RotateEvt(float angle)
    {

        Debug.Log(angle);

        if(this.angle == angle) tEvt?.Invoke();
        else fEvt?.Invoke();

    }

}
