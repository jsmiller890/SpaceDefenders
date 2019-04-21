using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score;
    private Text mytext;

    void Start()
    {
        mytext = GetComponent<Text>();
    }

    public void Score(int points)
    {
        Debug.Log("Scored points");
        score += points;
        mytext.text = "Score:" + score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
}
