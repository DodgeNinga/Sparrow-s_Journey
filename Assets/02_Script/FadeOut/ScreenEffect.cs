using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ScreenEffect : MonoBehaviour
{
    public Image panel;
    private PlayerMove playerMove;
    public string loadSceneName;

    public Sequence seqFadeOut;

    private void Awake()
    {

        playerMove = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();

    }

    private void Start()
    {

        playerMove.GetFade += FadeOut;

    }

    private void OnDestroy()
    {

        playerMove.GetFade -= FadeOut;

    }

    public void FadeOut()
    {

        print(panel.name);

        seqFadeOut = DOTween.Sequence()
            .SetAutoKill(false)
            .Append(panel.DOFade(1.0f, 2))
            .OnComplete(() =>
            {
                SceneManager.LoadScene(loadSceneName);
            });

    }


}
