using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickEvents : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Button soundButton;
    public TextMeshProUGUI soundsText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.mute)
        {
            //soundsText.text = "/";
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        } 
        else
        {
            //soundsText.text = "";
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMute()
    {
        if (GameManager.mute)
        {
            GameManager.mute = false;
            //soundsText.text = "";
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        } 
        else
        {
            GameManager.mute = true;
            //soundsText.text = "/";
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

   
    
}
