using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateSeq : MonoBehaviour
{

    [SerializeField] private UnityEvent endCb;
    [SerializeField] private Vector3 moveVec;
    [SerializeField] private float moveDel;
    [SerializeField] private Ease ease;
    [SerializeField] private float moveTime = 0.4f;

    public void MoveSeq()
    {

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(moveDel);
        seq.Append(transform.DOLocalRotate(moveVec, moveTime).SetEase(ease));
        seq.AppendCallback(() => endCb?.Invoke());

    }
}
