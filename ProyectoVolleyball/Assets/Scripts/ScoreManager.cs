using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

        // Cargar los valores desde PlayerPrefs
        targetPoints = PlayerPrefs.GetInt("SelectedPoints", 15); // Valor por defecto de 15 puntos
        targetSets = PlayerPrefs.GetInt("SelectedSets", 3); // Valor por defecto de 3 sets
    }

    // Método para sumar puntos al Jugador 1
    public void AddPointsP1()
    {
        scoreP1 += 1;
        scoreP1Text.text = scoreP1.ToString();

        // Verificar si alcanzó la cantidad de puntos para ganar un set
        if (scoreP1 >= targetPoints)
        {
            AddSetP1();
            ResetScores();
        }
    }

    // Método para sumar puntos al Jugador 2
    public void AddPointsP2()
    {
        scoreP2 += 1;
        scoreP2Text.text = scoreP2.ToString();

        // Verificar si alcanzó la cantidad de puntos para ganar un set
        if (scoreP2 >= targetPoints)
        {
            AddSetP2();
            ResetScores();
        }
    }

    // Método para sumar un set al Jugador 1
    private void AddSetP1()
    {
        setsP1 += 1;
        setsP1Text.text = setsP1.ToString();
        CheckForWinner();
    }

    // Método para sumar un set al Jugador 2
    private void AddSetP2()
    {
        setsP2 += 1;
        setsP2Text.text = setsP2.ToString();
        CheckForWinner();
    }

    // Método para verificar si alguien ha ganado el juego
    private void CheckForWinner()
    {
        if (setsP1 >= targetSets)
        {
            // Jugador 1 gana el juego
            SceneManager.LoadScene("Winner"); // Cambia a la escena de victoria
        }
        else if (setsP2 >= targetSets)
        {
            // Jugador 2 gana el juego
            SceneManager.LoadScene("Game Over"); // Cambia a la escena de derrota
        }
    }

    // Método para reiniciar los puntos
    private void ResetScores()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        scoreP1Text.text = scoreP1.ToString();
        scoreP2Text.text = scoreP2.ToString();
    }

    // Método para manejar el final del tiempo (debe ser llamado por el script Timer)
    public void HandleTimeEnd()
    {
        // Comparar puntos y otorgar un set al jugador con más puntos
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
            // Si los puntos son iguales, puedes decidir cómo manejar el empate
            Debug.Log("Empate: No se otorga un set.");
        }
    }
}
