using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public GameObject gameWelcomePanel;
    public GameObject gameConfigPanel;


    public TMP_Text setsText;
    public TMP_Text pointsText;
    public TMP_Text timeText;


    private int selectedSets = 3;
    private int selectedPoints = 15;
    private int selectedTime = 5;

   
    public void OpenSettings()
    {
        gameWelcomePanel.SetActive(false);
        gameConfigPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        gameConfigPanel.SetActive(false);
        gameWelcomePanel.SetActive(true);

        
        UpdateGameWelcomeTexts();
    }

   
    private void UpdateGameWelcomeTexts()
    {
        setsText.text = selectedSets.ToString();
        pointsText.text = selectedPoints.ToString();
        timeText.text = selectedTime.ToString() + ":00";
    }

    
    public void SelectSets(int sets)
    {
        selectedSets = sets;
    }

    public void SelectPoints(int points)
    {
        selectedPoints = points;
    }

    public void SelectTime(int time)
    {
        selectedTime = time;
    }

   
    public void QuitGame()
    {
        Application.Quit();
    }
}
