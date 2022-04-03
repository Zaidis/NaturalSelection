using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField] float delayTime;
    [SerializeField] bool delay;
    public void ChangeScenes(string newScene)
    {
        if (delay)
        {
            StartCoroutine(DelayedStart(newScene));
        }
        else
        {
            SceneManager.LoadScene(newScene);
        }
        
    }

    IEnumerator DelayedStart(string newScene)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        SceneManager.LoadScene(newScene);
    }

}
