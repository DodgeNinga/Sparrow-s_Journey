using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTest : MonoBehaviour
{

    [SerializeField] private string bgmName;

    private void Start()
    {
        SoundManager.instance.PlayBGM(bgmName);
    }
}
