using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPontos : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public bool buttonPressed;
    float clicked = 0;
    public AudioSource pontos;

    public GameObject GameManagerGO;

    void Start()
    {
        pontos = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            buttonPressed = true;
            pontos.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pontos.Play();
    }
}
