﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupFX : MonoBehaviour
{
    public List<GameObject> effects;

    private GameObject currentEffect;
    private GameObject nextEffect;
    private float fadeTime;
    private Color color1, color2;

    void Awake()
    {
        fadeTime = 0.25f;
        color1 = new Color(1f, 1f, 1f, 0f);
        color2 = new Color(1f, 1f, 1f, 1f);

        foreach(GameObject effect in effects)
        {
            effect.SetActive(false);
            effect.renderer.material.color = color1;
        }
    }

    public void Enable(float seconds)
    {
        int index = seconds > 10 ? 2 : seconds > 5 ? 1 : 0;
        currentEffect = effects[index];
        currentEffect.SetActive(true);
        currentEffect.renderer.material.color = color2;
        StartCoroutine(Countdown(seconds));
    }

    IEnumerator Countdown(float seconds)
    {
        yield return new WaitForSeconds(5f);
        while (GameManager.Instance.isPaused)
        {
            yield return new WaitForFixedUpdate();
        }
        seconds -= 5f;
        if(seconds >= 0f)
        {
            int index = ((int)seconds / 5) - 1;
            StartCoroutine(Fade(index));
            StartCoroutine(Countdown(seconds));
        }
    }

    IEnumerator Fade(int index)
    {
        if(index >= 0)
        {
            nextEffect = effects[index];
            nextEffect.SetActive(true);
        }

        float fadeCounter = fadeTime;

        while (fadeCounter >= 0f)
        {
            float delta = Time.deltaTime;
            float fadeAmount = delta / fadeTime;

            if (index >= 0)
            {
                color1.a += fadeAmount;
                nextEffect.renderer.material.color = color1;
            }

            color2.a -= fadeAmount;
            currentEffect.renderer.material.color = color2;

            fadeCounter -= delta;
            yield return new WaitForSeconds(delta);
            while (GameManager.Instance.isPaused)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        color2.a = 0f;
        currentEffect.renderer.material.color = color2;
        currentEffect.SetActive(false);

        if (index >= 0)
        {
            color1.a = 1f;
            nextEffect.renderer.material.color = color1;
            currentEffect = nextEffect;
        }

        color2.a = 1f;
        color1.a = 0f;
    }
}