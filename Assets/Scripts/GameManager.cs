using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static int currentLevelIndex;
    public static int numberOfPassedRings;
    public static bool mute = false;
    public static bool isGameStarted;
    public static int score = 0;
    public static string username;
    public static bool highscorePassed;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
   
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;
    public Slider gameProgressSlider;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    
    //public TextMeshProUGUI newHighscoreText;
   public ParticleSystem celebrationParticleEffect;
   public GameObject highscorePassedPanel;
    
    





    // Start is called before the first frame update


    private void Awake()
    {
       
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        PlayerPrefs.GetString("UserName");






    }
    void Start()
    {
        
        // Start the game
        Time.timeScale = 1;
        numberOfPassedRings = 0;

        // Set the text for the current highscore
        highscoreText.text = "Best Score:\n" + PlayerPrefs.GetInt("HighScore", 0);
        

        isGameStarted = gameOver = levelCompleted = highscorePassed = false;
    


        
        Social.localUser.Authenticate(ProcessAuthentication);






    }
   


    




    // Using social platform's  api to authenticate user
    void ProcessAuthentication (bool success)
    {
            if (success)
            {
            Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                "\nUser ID: " + Social.localUser.id +
                "\nIsUnderage: " + Social.localUser.underage;
            username = Social.localUser.userName;
            Debug.Log(userInfo);
            



        }
            else

                Debug.Log("Authentication failed");

    }


    // Update is called once per frame
    void Update()
    {

        //Update UI elements
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();


        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        #region standalone inputs

        // Comment this out if running on a smartphone

        /*if (Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }*/
        #endregion


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);


            if (score > PlayerPrefs.GetInt("HighScore"))
            {


                //celebrationParticleEffect.Play();

                highscorePassedPanel.SetActive(true);

            }
        }







        // Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                if (score > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                    PlayerPrefs.Save();




                }






                score = 0;

                SceneManager.LoadScene("Level");




            }
        }







            if (levelCompleted)
            {
                levelCompletedPanel.SetActive(true);

                if (Input.GetButtonDown("Fire1"))
                {
                    PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                    SceneManager.LoadScene("Level");
                }
            }

        }

    }

        




            






        

   
   

