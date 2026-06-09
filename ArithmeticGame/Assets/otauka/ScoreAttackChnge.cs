using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreAttackChange : MonoBehaviour
{
    public string NextSceneText;

    private AudioSource audioSource;

    public AudioClip ButtonSound;

    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }


    public void NextScene()
    {
        Quiz1.Boss = false;
        Quiz5.Boss5 = false;
        Quiz10.Boss10 = false;
        Quiz25.Boss25 = false;
        Quiz50.Boss50 = false;
        Quiz100.Boss100 = false;

        Quiz1.Score = true;
        Quiz5.Score5 = true;
        Quiz10.Score10 = true;
        Quiz25.Score25 = true;
        Quiz50.Score50 = true;
        Quiz100.Score100 = true;

        audioSource.PlayOneShot(ButtonSound);

        Invoke("Change", 1f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
