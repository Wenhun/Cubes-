using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score: Start";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        scoreText.text = "Score: " + amountToIncrease.ToString();
    }

}
