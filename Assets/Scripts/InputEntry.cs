using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]


public class InputEntry
{
    public string username;
    public int score;


    public InputEntry(string name, int score)
    {
        username = name;
        this.score = score;
    }

}
