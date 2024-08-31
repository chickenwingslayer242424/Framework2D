using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public TMP_Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        ShowHighScore();
    }

    // Update is called once per frame
    private void ShowHighScore()
    {
        int highScore = PlayerPrefs.GetInt("PlayerScore", 0);
        highScoreText.text = highScore.ToString();
    }
}
