using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        QuizUnlockManager.Heart1Clear = false;
        QuizUnlockManager.Heart5Clear = false;
        QuizUnlockManager.Heart10Clear = false;
        QuizUnlockManager.Heart25Clear = false;
        QuizUnlockManager.Heart50Clear = false;

        QuizUnlockManager.Shield1Clear = false;
        QuizUnlockManager.Shield5Clear = false;
        QuizUnlockManager.Shield10Clear = false;
        QuizUnlockManager.Shield25Clear = false;
        QuizUnlockManager.Shield50Clear = false;

        QuizUnlockManager.Sword1Clear = false;
        QuizUnlockManager.Sword5Clear = false;
        QuizUnlockManager.Sword10Clear = false;
        QuizUnlockManager.Sword25Clear = false;
        QuizUnlockManager.Sword50Clear = false;
    }

   
}
