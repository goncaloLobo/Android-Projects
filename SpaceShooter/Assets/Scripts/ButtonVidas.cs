using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonVidas : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    float clicked = 0;
    public AudioSource [] sounds;
    public AudioSource vidas; //sounds[0]
    public AudioSource vidas1; //sounds[1]
    public AudioSource vidas2; //sounds[2]
    public AudioSource vidas3; //sounds[3]

    public GameObject GameManagerGO;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        vidas = sounds[0];
        vidas1 = sounds[1];
        vidas2 = sounds[2];
        vidas3 = sounds[3];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            if(GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                if(!vidas.isPlaying)
                    vidas.Play();
            }
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
            {
                switch (PlayerControlSwipe.GetCurrentNumberOfLives())
                {
                    case 3:
                        if(!vidas3.isPlaying)
                            vidas3.Play();
                        break;
                    case 2:
                        if (!vidas2.isPlaying)
                            vidas2.Play();
                        break;
                    case 1:
                        if (!vidas1.isPlaying)
                            vidas1.Play();
                        break;
                }
            }
            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
        {
            if (!vidas.isPlaying)
                vidas.Play();
        }
        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
        {
            switch (PlayerControlSwipe.GetCurrentNumberOfLives())
            {
                case 3:
                    if (!vidas3.isPlaying)
                        vidas3.Play();
                    break;
                case 2:
                    if (!vidas2.isPlaying)
                        vidas2.Play();
                    break;
                case 1:
                    if (!vidas1.isPlaying)
                        vidas1.Play();
                    break;
            }
        }
    }
}
