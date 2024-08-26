using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public TextMeshProUGUI scoreP1Text;
    public TextMeshProUGUI scoreP2Text;

    int scoreP1 = 0;
    int scoreP2 = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreP1Text.text = scoreP1.ToString();
        scoreP2Text.text = scoreP2.ToString();

    }

    public void AddPointsP1()
    {
        scoreP1 += 1;
        scoreP1Text.text = scoreP1.ToString();
    }

    public void AddPointsP2()
    {
        scoreP2 += 1;
        scoreP2Text.text = scoreP2.ToString();
    }

}
