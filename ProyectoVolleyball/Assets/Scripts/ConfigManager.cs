using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    Transform welcomeObj;

    [SerializeField]
    Transform configObj;

    [Header("Texts")]
    [SerializeField]
    TextMeshProUGUI textSets;
    [SerializeField]
    TextMeshProUGUI textPoints;
    [SerializeField]
    TextMeshProUGUI textTime;

    [Header("Buttons")]
    [SerializeField]
    Button[] setButtons;
    [SerializeField]
    Button[] pointButtons;
    [SerializeField]
    Button[] timeButtons;

    [Header("Sprites")]
    [SerializeField]
    Sprite activeButtonSprite;
    [SerializeField]
    Sprite commonButtonSprite;

    bool _isInConfig = false;

    private Button activeSetButton;
    private Button activePointButton;
    private Button activeTimeButton;

    private void Start()
    {
        
        activeSetButton = setButtons[1];  
        activePointButton = pointButtons[1];
        activeTimeButton = timeButtons[1];

        foreach (var button in setButtons)
        {
            button.onClick.AddListener(() => OnSetButtonClick(button));
        }

        foreach (var button in pointButtons)
        {
            button.onClick.AddListener(() => OnPointButtonClick(button));
        }

        foreach (var button in timeButtons)
        {
            button.onClick.AddListener(() => OnTimeLimitButtonClick(button));
        }
    }

    #region Open / Close
    public void OpenCloseSettings()
    {
        _isInConfig = !_isInConfig;
        if(_isInConfig)
        {
            welcomeObj.gameObject.SetActive(false);
            configObj.gameObject.SetActive(true);
        }
        else
        {
            welcomeObj.gameObject.SetActive(true);
            configObj.gameObject.SetActive(false);
        } 
    }
    #endregion

    #region Set Variables

    private void OnSetButtonClick(Button button)
    {
        SetActiveButton(ref activeSetButton, button, textSets);
        LevelManager.Instance.SetSets(int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text));
    }

    private void OnPointButtonClick(Button button)
    {
        SetActiveButton(ref activePointButton, button, textPoints);
        LevelManager.Instance.SetPoints(int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text));
    }

    private void OnTimeLimitButtonClick(Button button)
    {
        SetActiveButton(ref activeTimeButton, button);
        int timeLimit = int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text);
        LevelManager.Instance.SetTime(timeLimit);
        textTime.text = FormatTimeLimit(timeLimit);
    }

    private void SetActiveButton(ref Button activeButton, Button newButton, TextMeshProUGUI displayText = null)
    {
        activeButton.GetComponent<Image>().sprite = commonButtonSprite;
        newButton.GetComponent<Image>().sprite = activeButtonSprite;
        activeButton = newButton;
        if (displayText != null)
        {
            displayText.text = newButton.GetComponentInChildren<TextMeshProUGUI>().text;
        }
    }

    private string FormatTimeLimit(int timeLimit)
    {
        int minutes = timeLimit;
        int seconds = 0;
        return $"{minutes:00}:{seconds:00}";
    }
    #endregion

}
