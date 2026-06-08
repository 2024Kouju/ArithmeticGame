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

        Quiz1.Boss = true;

        audioSource.PlayOneShot(ButtonSound);

        Invoke("Change", 1f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
