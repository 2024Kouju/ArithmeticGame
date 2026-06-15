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

    // 操作禁止にしたいボタン
    public Button[] buttons;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NextScene()
    {
        // 全ボタンを無効化
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }

        audioSource.PlayOneShot(ButtonSound);

        Invoke(nameof(Change), 5.5f);
    }

    public void Change()
    {
        SceneManager.LoadScene(NextSceneText);
    }
}