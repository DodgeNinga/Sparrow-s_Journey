using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{

    private readonly int WALKHASH = Animator.StringToHash("Walk");
    private Animator animator;
    private PlayerController playerController;

    public Action GetFade;

    private void Awake()
    {
        
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();

    }

    public void ExecuteMove(List<RoadClass> roads, Action endCallback)
    {

        Sequence seq = DOTween.Sequence();

        animator.SetBool(WALKHASH, true);
        transform.SetParent(null);

        foreach (var item in roads)
        {

            seq.AppendCallback(() =>
            {

                var dir = item.dir switch
                {
                    LookDir.front => item.road.transform.forward,
                    LookDir.back => -item.road.transform.forward,
                    LookDir.left => -item.road.transform.right,
                    LookDir.right => item.road.transform.right,
                    _ => transform.forward,

                };
                dir.y = 0;

                transform.forward = -dir;

            });
            seq.Append(transform.DOMove(item.road.GetMovePos(), 0.3f).SetEase(Ease.Linear));

            if (item.road.transform.CompareTag("Goal"))
            {

                seq.OnComplete(() =>
                {
                    GetFade?.Invoke();
                });

            }

        }

        seq.AppendCallback(() =>
        {

            animator.SetBool(WALKHASH, false);
            transform.SetParent(playerController.currentRoad.transform);
            endCallback();

        });

    }

}
