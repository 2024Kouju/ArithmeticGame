using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public string NextSceneText;

    void Start()
    {
        Invoke("NextScene", 3f);
    }

     public void NextScene()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
