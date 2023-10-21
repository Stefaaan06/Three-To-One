using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class deadCube : MonoBehaviour
{
    private PostProcessVolume postProcessLayer;
    private ChromaticAberration _ca;
    private void Start()
    {
        postProcessLayer = FindObjectOfType<PostProcessVolume>();
        postProcessLayer.profile.TryGetSettings(out _ca);

        StartCoroutine(deathEffect());
    }

    IEnumerator deathEffect()
    {
        float prevValue = _ca.intensity.value;
        _ca.intensity.value = 2;
        while (_ca.intensity.value > prevValue)
        {
            _ca.intensity.value -= 0.05f;
            yield return new WaitForSeconds(0.07f);
        }

        _ca.intensity.value = prevValue;
    }
}
