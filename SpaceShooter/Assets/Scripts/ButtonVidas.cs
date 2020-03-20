using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonVidas : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public bool buttonPressed;
    float clicked = 0;
    public AudioSource vidas;

    public GameObject GameManagerGO;

    void Start()
    {
        vidas = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            buttonPressed = true;
            vidas.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        vidas.Play();
    }
}
