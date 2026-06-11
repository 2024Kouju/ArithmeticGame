using UnityEngine;
using System;
using UnityEngine.UI;
public class DateDisplay : MonoBehaviour
{
    public Text dateText;

    void Start()
    {
        DateTime now = DateTime.Now;

        string[] week =
         {
            "“ú", "Œ", "‰Î", "…", "–Ø", "‹à", "“y"
        };

        dateText.text =
            now.Month + "\n" +
            "Œ\n" +
            now.Day + "\n" +
            "“ú\n" +
            "i" + week[(int)now.DayOfWeek] + "j";
    }
}
