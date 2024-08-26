using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreP1Text;
    public TextMeshProUGUI scoreP2Text;
    public TextMeshProUGUI setsP1Text;
    public TextMeshProUGUI setsP2Text;

    private int scoreP1 = 0;
    private int scoreP2 = 0;
    private int setsP1 = 0;
    private int setsP2 = 0;

    private int targetPoints;
    private int targetSets;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreP1Text.text = scoreP1.ToString();
        scoreP2Text.text = scoreP2.ToString();
        setsP1Text.text = setsP1.ToString();
        setsP2Text.text = setsP2.ToString();

        targetPoints = PlayerPrefs.GetInt("SelectedPoints", 15);
        targetSets = PlayerPrefs.GetInt("SelectedSets", 3);
    }

    public void AddPointsP1()
    {
        scoreP1 += 1;
        scoreP1Text.text = scoreP1.ToString();

        if (scoreP1 >= targetPoints)
        {
            AddSetP1();
            ResetScores();
        }
    }

    public void AddPointsP2()
    {
        scoreP2 += 1;
        scoreP2Text.text = scoreP2.ToString();

        if (scoreP2 >= targetPoints)
        {
            AddSetP2();
            ResetScores();
        }
    }

    private void AddSetP1()
    {
        setsP1 += 1;
        setsP1Text.text = setsP1.ToString();
        CheckForWinner();
    }

    private void AddSetP2()
    {
        setsP2 += 1;
        setsP2Text.text = setsP2.ToString();
        CheckForWinner();
    }

    private void CheckForWinner()
    {
        if (setsP1 >= targetSets)
        {
            LevelManager.Instance.LoadWinnerScene();
        }
        else if (setsP2 >= targetSets)
        {
            LevelManager.Instance.LoadGameOverScene();
        }
    }

    private void ResetScores()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        scoreP1Text.text = scoreP1.ToString();
        scoreP2Text.text = scoreP2.ToString();
    }

    public void HandleTimeEnd()
    {
        if (scoreP1 > scoreP2)
        {
            AddSetP1();
        }
        else if (scoreP2 > scoreP1)
        {
            AddSetP2();
        }
        else
        {
            Debug.Log("Empate: No se otorga un set.");
        }
    }
}
