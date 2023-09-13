using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DebugRotate : MonoBehaviour
{

    [SerializeField] private GameObject rotateObj;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            rotateObj.transform.DORotate(new Vector3(0, 0, 90), 0.5f).SetEase(Ease.Linear);

        }


    }

}
