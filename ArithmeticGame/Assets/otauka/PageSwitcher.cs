using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    public GameObject[] pages;

    private int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage);
    }

    public void NextPage()
    {
        // 次のページへ
        currentPage++;

        // 最後まで行ったら最初に戻る
        if (currentPage >= pages.Length)
        {
            currentPage = 0;
        }

        ShowPage(currentPage);
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }
}