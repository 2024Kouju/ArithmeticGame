using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public string NextSceneText;



     public void NextScene()
    {
        Invoke("Change", 1f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
