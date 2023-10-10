using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColButton : MonoBehaviour
{

    [SerializeField] private Vector3 inputPos;
    [SerializeField] private UnityEvent endEvt;

    private bool isInput = false;

    private void OnTriggerEnter(Collider other)
    {

        if (isInput) return;

        if (other.transform.CompareTag("Player"))
        {

            isInput = true;
            transform.DOLocalMove(inputPos, 0.3f).OnComplete(() => endEvt?.Invoke());

        }

    }

}
