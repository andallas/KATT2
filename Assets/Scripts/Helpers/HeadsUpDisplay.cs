using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeadsUpDisplay : MonoBehaviour
{
    public GameObject[] extraLives;

    private Text scoreText;
    private Text multiplierText;
    private string scoreString = "Score: ";

    void Awake()
    {
        Text[] childrenText = GetComponentsInChildren<Text>();
        
        int length = childrenText.Length;
        for(int i = 0; i < length; i++)
        {
            if(childrenText[i].gameObject.name == "Score")
            {
                scoreText = childrenText[i];
            }

            if (childrenText[i].gameObject.name == "Score Multiplier")
            {
                multiplierText = childrenText[i];
            }
        }

        if(scoreText == null)
        {
            Debug.LogError("Unable to find Score gameObject.");
        }

        if (multiplierText == null)
        {
            Debug.LogError("Unable to find Score Multiplier gameObject.");
        }
    }

    public void SetLives(int numLives)
    {
        int length = extraLives.Length;
        for (int i = 0; i < length; i++)
        {
            if(i < numLives)
            {
                extraLives[i].SetActive(true);
            }
            else
            {
                extraLives[i].SetActive(false);
            }
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = scoreString + score;
    }

    public void SetMultiplier(int multiplier)
    {
        multiplierText.text = multiplier.ToString();
    }
}