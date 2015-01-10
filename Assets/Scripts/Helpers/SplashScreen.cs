using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public float fadeTime;

    private Color color;
    private bool changeScene = false;
    private bool fadeIn = false;
    private bool fadeOut = false;


    void Start()
    {
        color = renderer.material.color;
        StartCoroutine("startFade");
    }

    void Update()
    {
        if(Input.GetButton("Cancel"))
        {
            LoadGame();
        }

        if (changeScene)
        {
            StartCoroutine("LoadMainMenu");
            changeScene = false;
        }
        else
        {
            if (fadeIn)
            {
                color.a += Time.deltaTime / fadeTime;
                renderer.material.color = color;

                if (color.a >= 1.0f)
                {
                    fadeIn = false;
                    StartCoroutine("FadeOut");
                }
            }
            else
            {
                if (fadeOut)
                {
                    color.a -= Time.deltaTime / fadeTime;
                    renderer.material.color = color;

                    if (color.a <= 0.0f)
                    {
                        changeScene = true;
                        fadeOut = false;
                    }
                }
            }
        }
    }

    IEnumerator startFade()
    {
        yield return new WaitForSeconds(1.25f);
        fadeIn = true;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.25f);
        fadeOut = true;
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(1.5f);
        LoadGame();
    }

    void LoadGame()
    {
        Application.LoadLevel("Main_Menu");
    }
}