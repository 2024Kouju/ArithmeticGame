using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goingtoschool : MonoBehaviour
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
        audioSource.PlayOneShot(ButtonSound);

        Invoke("Change", 5.5f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}
