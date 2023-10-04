using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instanse;
    public  static GameManager Instance
    {

        get 
        { 
            
            if(instanse == null) instanse = FindObjectOfType<GameManager>();

            return instanse; 
        
        }

    }

    private void Awake()
    {
        
        instanse = this;

    }

    public void Pause()
    {

        Time.timeScale = 0f;

    }

    public void Resume()
    {

        Time.timeScale = 1f;

    }

    public void Retry()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Exit()
    {

        SceneManager.LoadScene("StageSelect");

    }

}
