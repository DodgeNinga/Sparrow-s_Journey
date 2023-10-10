using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTest : MonoBehaviour
{
    SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    private void Update()
    {
        Click();
        Clear();
    }

    void Click()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            soundManager.PlayerSFX("Click");
        }
    }

    void Clear()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            soundManager.PlayerSFX("Clear");
            soundManager.StopBGM();
        }
    }
}
