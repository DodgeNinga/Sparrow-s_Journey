using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTest : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PlayBGM("Stage1");
    }
}
