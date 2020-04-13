using UnityEngine.UI;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public Text timeUI; // UI do contador de tempo

    public float startTime; // tempo quando o utilizador clica no jogar
    public float ellapsedTime; // tempo depois de o utilizador carregar no jogar
    public bool startCounter; // boolean para iniciar o contador
    public bool finishTime; // tempo final qdo o utilizador perde

    int minutes;
    int seconds;

    void Start()
    {
        startCounter = false;
        timeUI = GetComponent<Text>();
    }

    // função para iniciar o contador
    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }

    // funcao para terminar o contador
    public float StopTimeCounter()
    {
        startCounter = false;
        return ellapsedTime;
    }

    public void ResetTimer()
    {
        timeUI.text = string.Format("{0:00}:{1:00}", 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounter)
        {
            ellapsedTime = Time.time - startTime;
            minutes = (int) ellapsedTime / 60;
            seconds = (int) ellapsedTime % 60;

            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
