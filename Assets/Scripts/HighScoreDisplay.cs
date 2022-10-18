using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    //public GameObject[] scoresDisplay;


    public void DisplayHighScore(string name, int score)
    {
        nameText.text = name;
        //scoreText.text = string.Format("{0:000000}", score);
        scoreText.text = score.ToString();
    }


    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }


    
   

   

  

}
