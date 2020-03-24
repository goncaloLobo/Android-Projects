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

    // Start is called before the first frame update
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
    public static void SetHighScore(float highscore, int defesas)
    {
        PlayerPrefs.SetFloat("highscore", highscore);
        PlayerPrefs.SetInt("defesas", defesas);
        Debug.Log("highscore: " + highscore);
        Debug.Log("defesas: " + defesas);
    }
}
