using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{

    private readonly int WALKHASH = Animator.StringToHash("Walk");
    private Animator animator;

    private void Awake()
    {
        
        animator = GetComponentInChildren<Animator>();

    }

    public void ExecuteMove(List<RoadClass> roads, Action endCallback)
    {

        Sequence seq = DOTween.Sequence();

        animator.SetBool(WALKHASH, true);

        foreach(var item in roads)
        {

            seq.AppendCallback(() =>
            {

                var dir = item.dir switch
                {
                    LookDir.front => Vector3.forward,
                    LookDir.back => -Vector3.forward,
                    LookDir.left => -Vector3.right,
                    LookDir.right => Vector3.right,
                    _ => transform.forward,

                };
                dir.y = 0;

                transform.forward = -dir;

            });
            seq.Append(transform.DOMove(item.road.GetMovePos(), 0.3f).SetEase(Ease.Linear));

        }

        seq.AppendCallback(() =>
        {

            animator.SetBool(WALKHASH, false);
            endCallback();

        });

    }

}
