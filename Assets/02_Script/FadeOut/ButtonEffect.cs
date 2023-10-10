using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    private void Start()
    {
        fadeImage.enabled = false;
    }

    public void ButtonClick()
    {
        StartCoroutine(FadeRoutine());
    }

    public IEnumerator FadeRoutine()
    {
        fadeImage.enabled = true;
        float fadeCount = 0;
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeImage.color = new Color(0, 0, 0, fadeCount);
        }
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}