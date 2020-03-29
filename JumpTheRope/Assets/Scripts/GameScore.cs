using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreTextUI;
    public int score;

    public int Score
    {
        get{
            return this.score;
        }
        set{
            this.score = value;
            UpdateScoreTextUI();
        }
    }

    void Start()
    {
        scoreTextUI = GetComponent<Text>();
    }

    public void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:00000}", score);
        scoreTextUI.text = scoreStr;
    }

    // guarda os valores associados a um novo highscore
    public static void SetHighScore(float highscore, int saltos_perfeitos, int saltos_normais)
    {
        PlayerPrefs.SetFloat("highscore", highscore);
        PlayerPrefs.SetInt("perfeitos", saltos_perfeitos);
        PlayerPrefs.SetInt("normais", saltos_normais);
        PlayerPrefs.SetInt("total", saltos_perfeitos+saltos_normais);
        Debug.Log("highscore: " + highscore);
        Debug.Log("perfeitos: " + saltos_perfeitos);
        Debug.Log("normais: " + saltos_normais);
        Debug.Log("total: " + (saltos_perfeitos + saltos_normais));
    }
}
