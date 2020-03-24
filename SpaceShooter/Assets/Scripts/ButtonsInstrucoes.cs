using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsInstrucoes : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    private float clicked = 0;
    public AudioSource[] sounds;
    public AudioSource enemyShipSound; // sounds[0]
    public AudioSource descriptionSound; // sounds[1]
    public GameObject GameManagerGO;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        enemyShipSound = sounds[0];
        descriptionSound = sounds[1];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            enemyShipSound.Play();
            descriptionSound.PlayDelayed(enemyShipSound.clip.length);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        enemyShipSound.Play();
        descriptionSound.PlayDelayed(enemyShipSound.clip.length);
    }
}
