using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ambianceManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;
    void ambianceSelection()
    {
        int rand = Random.Range(0, clips.Length - 1);
        source.PlayOneShot(clips[rand], 0.3f);
    }
    private void Update()
    {
        if (!source.isPlaying)
        {
            ambianceSelection();
        }
    }

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
            StartCoroutine(decreaseSound());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(increaseSound());
        }
    }

    public IEnumerator increaseSound()
    {
        while (source.volume <= 0.5f)
        {
            source.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        source.volume = 0.5f;
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
