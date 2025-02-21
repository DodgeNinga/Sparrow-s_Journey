using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RotateHandle : MonoBehaviour
{
    public enum RotateOffset
    {

        X,
        Y,
        Z

    }

    [SerializeField] private RotateOffset offset;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform rotateObject;
    [SerializeField] private UnityEvent<float> rotateChangeEvent;
    [SerializeField] private UnityEvent rotateStartEvent, rotateEndEvent;
    [SerializeField] private bool containsRot = false;

    private PlayerController playerController;
    private Vector3 originPos;
    private List<RoadRoot> roads;
    private bool isKeyDown;
    private bool isAutoRotating = false;
    private float lastRotate;

    private float[] rotateArr => new float[] { 0, 90, 180, 270, 360 };

    private void Awake()
    {

        playerController = FindObjectOfType<PlayerController>();

        roads = rotateObject.GetComponentsInChildren<RoadRoot>().ToList();


    }

    
    private void Start()
    {

        var vec = GetRotateVec();
        rotateChangeEvent?.Invoke(vec.value);

    }

    private void Update()
    {

        if(isAutoRotating) return;

        if (isKeyDown) RotateObject();
        else return;

        if (Input.GetMouseButtonUp(0) && isAutoRotating == false)
        {
            
            isKeyDown = false;
            isAutoRotating = true;

            var vec = GetRotateVec();

            rotateObject.DORotate(vec.dir, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
            {

                isAutoRotating = false;
                rotateEndEvent?.Invoke();
                lastRotate = vec.value;

                if (vec.value == 360) vec.value = 0;

                rotateChangeEvent?.Invoke(vec.value);
                playerController.clickAble = true;


            });

        }

    }

    private void OnMouseDown()
    {

        rotateStartEvent?.Invoke();

        if (!containsRot)
        {

            if (roads.Contains(playerController.currentRoad)) return;

        }

        if (!playerController.clickAble) return;
        playerController.clickAble = false;
        isKeyDown = true;
        originPos = Input.mousePosition;

    }

    private void OnDisable()
    {

        rotateChangeEvent?.Invoke(33333);

    }

    private (Vector3 dir, float value) GetRotateVec()
    {

        float curRotate = offset switch
        {

            RotateOffset.X => GetXVAL(),
            RotateOffset.Y => rotateObject.eulerAngles.y,
            RotateOffset.Z => rotateObject.eulerAngles.z,
            _ => 0

        };

        float min = rotateArr.Min(x => Mathf.Abs(x - curRotate));
        float value = rotateArr.First(x => Mathf.Abs(x - curRotate) == min);

        Debug.Log(transform.localEulerAngles.x);


        return offset switch
        {

            RotateOffset.X => (new Vector3(value, 0, 0), value),
            RotateOffset.Y => (new Vector3(0, value, 0), value),
            RotateOffset.Z => (new Vector3(0, 0, value), value),
            _ => (Vector3.zero, 0)

        };

        float GetXVAL()
        {


            var mousePos = Input.mousePosition;
            var pos = mousePos - originPos;

            var value = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;

            return value + lastRotate;

        }

    }

    private void RotateObject()
    {

        switch (offset)
        {

            case RotateOffset.X:
                {

                    var mousePos = Input.mousePosition;
                    var pos = mousePos - originPos;

                    var value = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;

                    rotateObject.rotation = Quaternion.AngleAxis(value + lastRotate, Vector3.right);

                    break;

                }

            case RotateOffset.Y:
                {
                    var v = (Input.mousePosition - originPos);

                    var value = -(v.x + v.y) * Time.deltaTime * rotateSpeed;

                    rotateObject.Rotate(new Vector3(0, value, 0));

                    originPos = Input.mousePosition;

                    break;

                }

            case RotateOffset.Z:
                {

                    var mousePos = Input.mousePosition;
                    var pos = mousePos - originPos;

                    var value = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;

                    rotateObject.rotation = Quaternion.AngleAxis(value + lastRotate, Vector3.forward);

                    break;

                }

        }

    }

}
