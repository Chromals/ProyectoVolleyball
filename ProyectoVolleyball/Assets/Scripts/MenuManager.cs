using UnityEngine;
using UnityEngine.UI;
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

   
    private Button selectedSetsButton;
    private Button selectedPointsButton;
    private Button selectedTimeButton;


    public Sprite selectedButtonSprite;
    private Sprite originalButtonSprite;

   
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
       
        UpdateButtonSelection(ref selectedSetsButton, UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
    }

    
    public void SelectPoints(int points)
    {
        selectedPoints = points;
        
        UpdateButtonSelection(ref selectedPointsButton, UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
    }

  
    public void SelectTime(int time)
    {
        selectedTime = time;
       
        UpdateButtonSelection(ref selectedTimeButton, UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
    }

 
    private void UpdateButtonSelection(ref Button previousSelectedButton, Button newSelectedButton)
    {
        
        if (previousSelectedButton != null)
        {
            previousSelectedButton.GetComponent<Image>().sprite = originalButtonSprite;
        }

       
        previousSelectedButton = newSelectedButton;

       
        originalButtonSprite = newSelectedButton.GetComponent<Image>().sprite;

     
        newSelectedButton.GetComponent<Image>().sprite = selectedButtonSprite;
    }

  
    public void QuitGame()
    {
        Application.Quit();
    }
}
