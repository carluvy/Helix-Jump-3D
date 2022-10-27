using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    private int selectedCharacter = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ch in characters)
        {
            ch.SetActive(false);

        }
        characters[selectedCharacter].SetActive(true);
    }

    public void ChangeCharacter(int newCharacter)
    {
        if (PlayerPrefs.GetInt("HighScore", 0) > 10)
        {

            characters[selectedCharacter].SetActive(false);
            characters[newCharacter].SetActive(true);
            selectedCharacter = newCharacter;
        }
        


       
        
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
