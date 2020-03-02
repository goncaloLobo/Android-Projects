using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GKControl;
    public GameObject playButton;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                playButton.SetActive(true);
                break;
            
            case GameManagerState.Gameplay:
                playButton.SetActive(false);
                GKControl.GetComponent<GKControl>().ScheduleKicks(2);

                break;
            case GameManagerState.GameOver:
                //mudar o estado do gamemanagerstate
                Invoke("ChangeToOpeningState", 8f);

                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    // botao play ira chamar esta funcao
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
