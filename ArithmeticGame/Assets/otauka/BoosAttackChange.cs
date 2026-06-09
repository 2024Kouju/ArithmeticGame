using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoosAttackChange : MonoBehaviour
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

        Quiz1.Score = false;
        Quiz5.Score5 = false;
        Quiz10.Score10 = false;
        Quiz25.Score25 = false;
        Quiz50.Score50 = false;
        Quiz100.Score100 = false;

        Quiz1.Boss = true;
        Quiz5.Boss5 = true;
        Quiz10.Boss10 = true;
        Quiz25.Boss25 = true;
        Quiz50.Boss50 = true;
        Quiz100.Boss100 = true;

        audioSource.PlayOneShot(ButtonSound);

        Invoke("Change", 1f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
