using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] Image fadePanel;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI percentText;

    public string loadSceneName;

    private float percent = 100;

    private void Awake()
    {
        percent = 0;
        percentText.text = "0%";
        progressBar.fillAmount = 0;

        Color tempColor = fadePanel.color;
        tempColor.a = 1;
        fadePanel.color = tempColor;

        StartCoroutine(LoadingProcess());
    }

    private IEnumerator LoadingProcess()
    {
        var swfs = new WaitForSeconds(0.05f);
        var lwfs = new WaitForSeconds(0.5f);

        fadePanel.DOFade(0, 0.5f);
        yield return lwfs;
        
        for(int i = 0; i<100; i++)
        {
            percent++;
            percentText.text = $"{percent}%";
            progressBar.fillAmount = percent / 100;
            yield return swfs;
        }

        fadePanel.DOFade(1, 0.5f);
        yield return lwfs;
        SceneManager.LoadScene(loadSceneName);
    }
}
