using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public MonoBehaviour[] stopScripts;
    public float stopTime = 3f;

    IEnumerator Start()
    {
        // ŠJŽn’¼Œã‚É’âŽ~
        foreach (MonoBehaviour script in stopScripts)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        yield return new WaitForSeconds(stopTime);

        foreach (MonoBehaviour script in stopScripts)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }
    }
}