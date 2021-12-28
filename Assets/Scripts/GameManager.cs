using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private SceneFader _sceneFader;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        DontDestroyOnLoad(this);
    }

    public static void PlayerDied()
    {
        if (instance._sceneFader)
        {
            instance._sceneFader.FadeOut();
        }
        else
        {
            Debug.LogError("SceneFader 未注册");
        }
        
        instance.Invoke("RestartScene" , 1.5f);
    }

    public static void RegisterSceneFader(SceneFader sceneFader)
    {
        instance._sceneFader = sceneFader;
        
        
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
