 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger _inst;
    public float globalSpeed =1f;

    public void Awake()
    {
        _inst = this;
    }
    private void Update()
    {
        if(globalSpeed < 5f)
        globalSpeed += Time.deltaTime * 0.01f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Info()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
