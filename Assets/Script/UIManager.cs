using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    float score = 0;
    public TextMeshProUGUI scoreTMP,HighScoreTMP;

    private void Start()
    {
        HighScoreTMP.text = "High Score: " + PlayerPrefs.GetFloat("HighScore",0).ToString("0");
    }
    private void Update()
    {
        score += Time.deltaTime;
        scoreTMP.text = score.ToString("0.00");
    }
    private void OnDestroy()
    {
        if (score > PlayerPrefs.GetFloat("HighScore",score))
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
    }

}
