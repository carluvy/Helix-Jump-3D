using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static int currentLevelIndex;
    public static int numberOfPassedRings;
    public static bool mute = false;
    public static bool isGameStarted;
    public static int score = 0;
    
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;
    public Slider gameProgressSlider;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public GameObject[] userPrefab;
  



    // Start is called before the first frame update


    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }
    void Start()
    {
        Time.timeScale = 1;
        numberOfPassedRings = 0;
        highscoreText.text = "Best Score:\n" + PlayerPrefs.GetInt("HighScore", 0);
        Social.ShowLeaderboardUI();

        isGameStarted = gameOver = levelCompleted = false;







        Social.localUser.Authenticate(ProcessAuthentication);
        /*leaderboard = Social.CreateLeaderboard();
        
        
        leaderboard.id = "HelixLeaderboard01";
*/
       










    }

    void CreateLeaderBoard()
    {
        
        //leaderboard.LoadScores(result => DidLoadLeaderboard(result));
    }


   /* void DidLoadLeaderboard(bool result)
    {
        Debug.Log("Received " + leaderboard.scores.Length + " scores");
        foreach (IScore score in leaderboard.scores)
            Debug.Log(score);
    }*/


    void ProcessAuthentication (bool success)
    {
            if (success)
            {
            Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                "\nUser ID: " + Social.localUser.id +
                "\nIsUnderage: " + Social.localUser.underage;
            Debug.Log(userInfo);
            // Request loaded achievements, and register a callback for processing them
            //Social.LoadAchievements(ProcessLoadedAchievements);



        }
            else

                Debug.Log("Authentication failed");

    }

    // This function gets called when the LoadAchievement call completes
    void ProcessLoadedAchievements(IAchievement[] achievements)
    {
        if (achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            Debug.Log("Got " + achievements.Length + " achievements");

        // You can also call into the functions like this
        Social.ReportProgress("Achievement01", 100.0, result => {
            if (result)
                Debug.Log("Successfully reported achievement progress");
            else
                Debug.Log("Failed to report achievement");
        });

        long scoreLong  = PlayerPrefs.GetInt("HighScore");

       
        Social.ReportScore(scoreLong, "HelixLeaderboard01", HighScoreCheck);
        
        
    }


    void HighScoreCheck(bool result)
    {
        if (result)
            Debug.Log("Successfully reported score progress");
         else
            Debug.Log("Failed to report score");
        }
// Update is called once per frame
void Update()
    {
       /* Social.ReportScore(PlayerPrefs.GetInt("HighScore"), "HelixLeaderboard01", success =>
        {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });*/
        //Update UI elements
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        #region standalone inputs
        if (Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }
        #endregion


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;
          
            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);

        }
        
        // Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                if (score > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }

                score = 0;

                SceneManager.LoadScene("Level");
               
                /* Social.ReportScore(PlayerPrefs.GetInt("HighScore"), "HelixLeaderboard01", success => {
                     if (success)
                     {
                         Debug.Log("Succesful");
                     } else
                     {
                         Debug.Log("failed to report");
                     }
                     });*/
                //Social.LoadAchievements(ProcessLoadedAchievements);
              


                //ReportScore(PlayerPrefs.GetInt("HighScore"), "HelixLeaderboard01");
            }

            /*Social.LoadScores("HelixLeaderboard01", scores =>
            {
                if (scores.Length > 0)
                {
                    Debug.Log("Got " + scores.Length + " scores");
                    string myScores = "Leaderboard:\n";
                    foreach (IScore score in scores)
                        myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
                    Debug.Log(myScores);

                }
                else
                    Debug.Log("No scores loaded");
            });*/


            /* leaderboard.LoadScores(result =>
             {
                 Debug.Log("Received " + leaderboard.scores.Length + " scores");
                 foreach (IScore score in leaderboard.scores)
                     Debug.Log(score.value);


             });


         leaderboardScores.text = leaderboard.localUserScore.value.ToString();*/
            //leaderboardScores.text = Social.LoadScores("HelixLeaderboard01", );

            //ReportScore(leaderboard.localUserScore.value, leaderboard.id);

            //Social.ShowLeaderboardUI();
        }



            
        


     if (levelCompleted)
        {
            levelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex +1);
                SceneManager.LoadScene("Level");
            }
        }
    }

    private void ReportScore (long score, string leaderboardID)
    {
        Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
        Social.ReportScore(score, leaderboardID, success =>
        {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });
    }

   /* private void onLeaderboardLoadComplete()
    {
        leaderboard.LoadScores(result =>
        {
            Debug.Log("Received " + leaderboard.scores.Length + " scores");
            foreach (IScore score in leaderboard.scores)
            {

                leaderboardScores.text = score.ToString();
                Debug.Log(score);
            }



        });

        

    }*/
}
