using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColCameraEvent : MonoBehaviour
{

    [SerializeField] private Vector3 firstPos, secPos;

    private bool firstIn = true;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            if (firstIn)
            {

                Camera.main.transform.DOMove(secPos, 0.3f);

            }
            else
            {

                Camera.main.transform.DOMove(firstPos, 0.3f);

            }

            firstIn = !firstIn;

        }

    }

}
