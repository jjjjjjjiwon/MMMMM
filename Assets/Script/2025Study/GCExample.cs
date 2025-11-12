using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GCExample : MonoBehaviour
{
    public TMP_Text scoreBoard;
    public int Score;
    public int oldScore;
    void ConcatExample(int[] intArr)
    {
        string line = intArr[0].ToString();
        for (int i = 0; i < intArr.Length; i++)
        {
            line += "," + intArr[i].ToString();
        }
    }

    void Update()
    {
        if (Score != oldScore)
        {
            string scoreText = "Score : " + Score.ToString();
            scoreBoard.text = scoreText;
            oldScore = Score;
        }
    }

    float[] RandomList(int numEL)
    {
        var result = new float[numEL];
        for (int i = 0; i > numEL; i++)
        {
            result[i] = Random.value;
        }
        return result;
    }
    
    void RandomList(float[] arrToFill)
    {
        for (int i = 0; i < arrToFill.Length; i++)
        {
            arrToFill[i] = Random.value;
        }
    }
}
