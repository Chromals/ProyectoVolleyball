using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private bool timerEnded = false;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (remainingTime <= 0 && !timerEnded)
        {
            
            remainingTime = 0;
            timerText.text = "00:00";
            timerText.color = Color.red;
            timerEnded = true; 
            GameOver();
        }
    }

    private void GameOver()
    {
        
        SceneManager.LoadScene("Game Over");
    }
}
