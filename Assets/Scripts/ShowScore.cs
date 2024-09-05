using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text highScoreName;
    // private int currentScore;   
    // Start is called before the first frame update
    void Start()
    {
        
    //    currentScore = GetCurrentScore();
        ShowHighScore();
        ShowPlayerName();
    }
//    private int GetCurrentScore()
//     {
//       return GameManager.instance.GetScore();
      
//     }
    // private string GetCurrentName()
    // {
    //     return GameManager.instance.GetName();
    // }
    // private void UpdateHighScore()
    // {
    //      int highScore = PlayerPrefs.GetInt("PlayerScore", 0);
    //      if (currentScore > highScore)
    //      {
    //          PlayerPrefs.SetInt("PlayerScore", currentScore);
    //         PlayerPrefs.SetString("PlayerName", GetCurrentName());
    //      }
    // }

    // Update is called once per frame
    private void ShowHighScore()
    {
        int highScore = PlayerPrefs.GetInt("PlayerScore");
        highScoreText.text = highScore.ToString();
    }
    private void ShowPlayerName()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        highScoreName.text = playerName;

    }
}
