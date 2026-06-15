using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    // í‚é~ÇµÇΩÇ¢ÉXÉNÉäÉvÉgÇìoò^
    public MonoBehaviour[] stopScripts;

    // í‚é~éûä‘
    public float stopTime = 3f;

    void Start()
    {
        StartCoroutine(StopScriptsTemporarily());
    }

    IEnumerator StopScriptsTemporarily()
    {
        // í‚é~
        foreach (MonoBehaviour script in stopScripts)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        yield return new WaitForSeconds(stopTime);

        // çƒäJ
        foreach (MonoBehaviour script in stopScripts)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }
    }
}