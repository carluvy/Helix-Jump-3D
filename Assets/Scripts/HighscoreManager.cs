using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
   
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private GameObject layoutContainer;

    public InputField inputField;
    [SerializeField] string filename;
    readonly List<InputEntry> entriesList = new List<InputEntry>();
    List<InputEntry> jsonList = new List<InputEntry>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        //layoutContainer = GameObject.Find("/Canvas/StartMenuPanel/MainMenu/ListContainer/ViewPort/LayoutContainer");

        
        // Assign dummy username if user has not entered a username yet
        if (PlayerPrefs.GetString("UserName").Length.Equals(0))
        {
            PlayerPrefs.SetString("UserName", GameManager.username);
        } 

        //Adds some test data  on first run
        /*AddNewScore("John", Random.Range(1, 100));
        AddNewScore("Max", Random.Range(1, 100));
        AddNewScore("Dave", Random.Range(1, 100));
        AddNewScore("Steve", Random.Range(1, 100));
        AddNewScore("Mike", Random.Range(1, 100));
        AddNewScore("Teddy", Random.Range(1, 100));
        AddNewScore("Wade", Random.Range(1, 100));
        AddNewScore("Canary", Random.Range(1, 100));
        AddNewScore("Mia", Random.Range(1, 100));
        AddNewScore("Arrow", Random.Range(1, 100));*/

        SpawnListItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnListItems()
    {
        // Update the UI with data from the JSON file
        
        jsonList = FileHandler.ReadListFromJSON<InputEntry>(filename);

        jsonList = jsonList.Distinct().ToList();
        jsonList.Sort((InputEntry x, InputEntry y) => y.score.CompareTo(x.score));


        foreach (InputEntry item in jsonList)
        {
            GameObject listIem = Instantiate(itemPrefab);
            listIem.transform.SetParent(layoutContainer.transform, false);

            Transform usernameTextTransform = listIem.transform.Find("name");
            Transform scoreTextTransform = listIem.transform.Find("score");

            TextMeshProUGUI usernameText = usernameTextTransform.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = scoreTextTransform.GetComponent<TextMeshProUGUI>();

            usernameText.text = item.username;
            scoreText.text = item.score.ToString();
        }

        /*for (int i = 0; i < highScoreDisplayArray.Count; i++)
        {
            if (i < jsonList.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(jsonList[i].username, jsonList[i].score);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }*/

    }


    // Add username from user input to the list and save the list to the Json file
    public void AddUserInput()
    {

        PlayerPrefs.SetString("UserName", inputField.text);
        jsonList.Add(new InputEntry(PlayerPrefs.GetString("UserName"), PlayerPrefs.GetInt("HighScore", 0)));


        //entriesList.ForEach((item) => userInfo.Add(new HighScoreEntry { name = item.username, score = item.score }));
        inputField.text = "";
        
        FileHandler.SaveToJSON(jsonList, filename);

    }


    // Manually add new highscore entries the first time the game runs
    public void AddNewScore(string entryName, int entryScore)
    {
        jsonList.Add(new InputEntry(entryName, entryScore));

        FileHandler.SaveToJSON(jsonList, filename);

    }
}
