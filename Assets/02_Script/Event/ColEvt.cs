using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColEvt : MonoBehaviour
{

    public UnityEvent evt;

    private void OnTriggerEnter(Collider other)
    {

        evt?.Invoke();

    }

}
