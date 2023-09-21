using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{
    public Action GetFade;

    public void ExecuteMove(List<RoadRoot> roads, Action endCallback)
    {

        Sequence seq = DOTween.Sequence();

        foreach (var item in roads)
        {

            seq.Append(transform.DOMove(item.GetMovePos(), 0.3f).SetEase(Ease.Linear));

            if (item.transform.CompareTag("Goal"))
            {

                seq.OnComplete(() =>
                {
                    GetFade?.Invoke();
                });

            }

        }

        seq.AppendCallback(() => endCallback());

    }

}
