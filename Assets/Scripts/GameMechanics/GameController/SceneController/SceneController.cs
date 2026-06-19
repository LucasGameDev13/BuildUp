using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public event Action<int> OnSceneLoaded;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    public int MySceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnSceneLoaded?.Invoke(scene.buildIndex);
    }
}
