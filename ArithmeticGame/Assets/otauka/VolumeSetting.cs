using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider bgmSlider;
    public Slider seSlider;

    void Start()
    {
        // 保存した音量を読み込む
        float bgm = PlayerPrefs.GetFloat("BGM", 1f);
        float se = PlayerPrefs.GetFloat("SE", 1f);

        bgmSlider.value = bgm;
        seSlider.value = se;

        SetBGM(bgm);
        SetSE(se);

        // スライダー変更時に呼び出す
        bgmSlider.onValueChanged.AddListener(SetBGM);
        seSlider.onValueChanged.AddListener(SetSE);
    }

    public void SetBGM(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        mixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("BGM", value);
    }

    public void SetSE(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        mixer.SetFloat("SEVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("SE", value);
    }
}