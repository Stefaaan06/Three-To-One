using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicManager : MonoBehaviour
{
    public AudioSource source;
    
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex <= 1)
        {
            StopAllCoroutines();
            StartCoroutine(increaseSound());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(decreaseSound());
        }
    }

    public IEnumerator increaseSound()
    {
        while (source.volume <= 0.1f)
        {
            source.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        source.volume = 0.1f;
    }

    public IEnumerator decreaseSound()
    {
        while (source.volume >= 0f)
        {
            source.volume -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        source.volume = 0;
    }
}
